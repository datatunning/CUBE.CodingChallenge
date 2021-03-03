import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HealthClientService {
  constructor(private http: HttpClient) { }

  public check() {
    return this.http.get(`${environment.apiEndpoint}/health`, { responseType: 'text'});
  }
}
