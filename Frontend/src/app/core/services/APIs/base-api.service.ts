import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddAdmin } from '../../models/AddAdmin';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BaseApiService {
  constructor(protected http: HttpClient) {}

  getAdmins() {
    return this.http.get<any>(
      `${environment.apiUrl}/MinistryOfHealth/governorate-admins`
    );
  }
  addAdmin(model: AddAdmin) {
    return this.http.post<any>(
      `${environment.apiUrl}/MinistryOfHealth/add-governorate-admin`,
      model
    );
  }
}
