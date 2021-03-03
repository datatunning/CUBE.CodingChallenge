import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Alert } from 'src/app/models/Alert.model';
import { AlertTypes } from 'src/app/models/AlertTypes.enum';
import { TemperatureConversionModes } from 'src/app/models/TemperatureConversion.mode';
import { TemperatureConverterResponse } from 'src/app/models/TemperatureConverter.response';
import { TemperatureUnits } from 'src/app/models/TemperatureUnits.enum';
import { AlertService } from 'src/app/services/Alert.service';
import { TemperatureClientService } from 'src/app/services/TemperatureClient.service';

@Component({
  selector: 'cube-temperature-converter',
  templateUrl: './temperature-converter.component.html',
  styleUrls: ['./temperature-converter.component.scss']
})
export class TemperatureConverterComponent {
  fromUnit: TemperatureUnits;
  toUnit: TemperatureUnits;
  convertedValue: number;

  conversionForm = this.fb.group({
    conversionMode: [null, Validators.required],
    fromValue: [null, Validators.required]
  });

  // Allow to use the enum value along with associated enum string in the template.
  temperatureConversionModes: string[] = Object.values(TemperatureConversionModes).filter((key) => key);

  // Allow to use the enum items in template for comparison with enum values.
  temperatureConversionModesType = TemperatureConversionModes;

  constructor(
    private fb: FormBuilder,
    private temperatureClient: TemperatureClientService,
    private alertService: AlertService
  ) {}

  onTemperatureModeChanged(mode: TemperatureConversionModes) {
    switch (mode) {
      case TemperatureConversionModes.celsiusFahrenheit:
        this.fromUnit = TemperatureUnits.celsius;
        this.toUnit = TemperatureUnits.fahrenheit;
        break;
      case TemperatureConversionModes.celsiusKelvin:
        this.fromUnit = TemperatureUnits.celsius;
        this.toUnit = TemperatureUnits.kelvin;
        break;
      case TemperatureConversionModes.fahrenheitCelsius:
        this.fromUnit = TemperatureUnits.fahrenheit;
        this.toUnit = TemperatureUnits.celsius;
        break;
      case TemperatureConversionModes.fahrenheitKelvin:
        this.fromUnit = TemperatureUnits.fahrenheit;
        this.toUnit = TemperatureUnits.kelvin;
        break;
      case TemperatureConversionModes.kelvinCelsius:
        this.fromUnit = TemperatureUnits.kelvin;
        this.toUnit = TemperatureUnits.celsius;
        break;
      case TemperatureConversionModes.kelvinFahrenheit:
        this.fromUnit = TemperatureUnits.kelvin;
        this.toUnit = TemperatureUnits.fahrenheit;
        break;
    }

    this.convertedValue = null;
  }

  onSubmit() {
    this.temperatureClient
        .convert(this.fromUnit, this.conversionForm.get('fromValue').value, this.toUnit)
        .subscribe(
          (response: TemperatureConverterResponse) => {
            this.convertedValue = response.toTemperature;
          },
          (error) => {
            const alert: Alert = {
              title: 'Error',
              type: AlertTypes.error,
              message: error.message,
              timestamp: new Date()
            };
            this.alertService.push(alert);
          }
        );
  }
}
