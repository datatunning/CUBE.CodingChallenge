import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TemperatureConverterResponse } from '../models/TemperatureConverter.response';
import { TemperatureUnits } from '../models/TemperatureUnits.enum';

@Injectable({
  providedIn: 'root'
})
export class TemperatureClientService {
  constructor(private http: HttpClient) { }

  public convert(fromUnit: TemperatureUnits, fromValue: number, toUnit: TemperatureUnits) {
    return this.http.get<TemperatureConverterResponse>(
      `${environment.apiEndpoint}/convert?FromUnit=${fromUnit}&FromTemperature=${fromValue}&ToUnit=${toUnit}`);
  }
}
