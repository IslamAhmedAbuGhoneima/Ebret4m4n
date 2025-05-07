import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AddAdmin } from '../../../core/models/AddAdmin';

@Injectable({
  providedIn: 'root',
})
export class HealthMinistryService {
  constructor(private http: HttpClient) {}

  getGovernorates() {
    return this.http.get<any>(
      `${environment.apiUrl}/MinistryOfHealth/governorates`
    );
  }

  getGovernorateDetails(governorateName: string) {
    const params = new HttpParams().set('governorateName', governorateName);

    return this.http.get<any>(
      `${environment.apiUrl}/MinistryOfHealth/governorate-details`,
      { params }
    );
  }

  getStatisticsOfAdmin() {
    return this.http.get<any>(`${environment.apiUrl}/Statistics/admin`);
  }
}
