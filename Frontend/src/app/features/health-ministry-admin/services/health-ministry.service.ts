import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, OnInit, model } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AddAdmin } from '../../../core/interfaces/AddAdmin';

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
  getCityCenterDetails(cityAdminId: string) {
    return this.http.get<any>(
      `${environment.apiUrl}/MinistryOfHealth/${cityAdminId}/cities-healthcares-datails`
    );
  }

  getStatisticsOfAdmin() {
    return this.http.get<any>(`${environment.apiUrl}/Statistics/admin`);
  }

  getGovernoratesAdmins() {
    return this.http.get<any>(
      `${environment.apiUrl}/MinistryOfHealth/governorate-admins`
    );
  }
  addGovernorateAdmin(model: AddAdmin) {
    return this.http.post<any>(
      `${environment.apiUrl}/MinistryOfHealth/add-governorate-admin`,
      model
    );
  }
  getGovernorateAdminDetails(governorateAdminId: any) {
    return this.http.get<any>(
      `${environment.apiUrl}/MinistryOfHealth/${governorateAdminId}/governorate-admin-details`
    );
  }
  editGovernorateAdmin(
    governorateAdminId: any,
    model: {
      firstName: string;
      lastName: string;
      email: string;
      governorate: string;
    }
  ) {
    return this.http.put<any>(
      `${environment.apiUrl}/MinistryOfHealth/${governorateAdminId}/update-governorate-admins`,
      model
    );
  }
}
