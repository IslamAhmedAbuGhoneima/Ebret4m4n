import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class GovernorateAdminService {
  constructor(private http: HttpClient) {}

  getStatisticsOfGovernorateAdmin() {
    return this.http.get<any>(`${environment.apiUrl}/Statistics/governorate`);
  }
  getCitiesCenters() {
    return this.http.get<any>(`${environment.apiUrl}/GovernorateAdmin/cities`);
  }
  getCityCenterDetails(centerName: any) {
    const params = new HttpParams().set('cityName', centerName);
    return this.http.get<any>(
      `${environment.apiUrl}/GovernorateAdmin/city-details`,
      { params }
    );
  }
  getCitiesCenterAdmins() {
    return this.http.get<any>(
      `${environment.apiUrl}/GovernorateAdmin/city-admins`
    );
  }
  addCityCenterAdmin(model: {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    governorate: string;
    city: string;
  }) {
    return this.http.post<any>(
      `${environment.apiUrl}/GovernorateAdmin/city-admin-add`,
      model
    );
  }
  getAdminDetails(cityAdminId: any) {
    return this.http.get<any>(
      `${environment.apiUrl}/GovernorateAdmin/${cityAdminId}/city-admin-data`
    );
  }
  editCityCenterAdmin(cityAdminId: string, model: any) {
    return this.http.put<any>(
      `${environment.apiUrl}/GovernorateAdmin/${cityAdminId}/update-city-admin`,
      model
    );
  }
}
