import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CityCenterService {
  constructor(private http: HttpClient) {}
  getHealthCareUnitDetails(healthUnitId: string) {
    return this.http.get<any>(
      `${environment.apiUrl}/CityAdmin/${healthUnitId}/healthCareCenter`
    );
  }
}
