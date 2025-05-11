import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AddAdmin } from '../../interfaces/AddAdmin';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class GlobalService {
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
  getVaccines() {
    return this.http.get<any>(
      `${environment.apiUrl}/Child/child-base-vaccines`
    );
  }
}
