import { TemperatureUnits } from './TemperatureUnits.enum';

export interface TemperatureConverterResponse {
  fromUnit: TemperatureUnits;
  fromTemperature: number;
  toUnit: TemperatureUnits;
  toTemperature: number;
}
