import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  constructor(private http: HttpClient) {}

  childrenDisease() {
    return this.http.get<any>(`${environment.apiUrl}/Doctor/children-disease`);
  }
  childDiseaseDetails(childId: any) {
    return this.http.get<any>(
      `${environment.apiUrl}/Doctor/${childId}/child-data`
    );
  }
  
  childrenSuspended() {
    return this.http.get<any>(
      `${environment.apiUrl}/Doctor/children-suspended`
    );
  }
  childSuspended(childId:any) {
    return this.http.get<any>(
      `${environment.apiUrl}/Doctor/${childId}/suspend`
    );
  }
}
