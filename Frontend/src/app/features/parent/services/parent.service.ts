import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, model } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ParentService {
  constructor(protected http: HttpClient) {}

  getMyChildren() {
    return this.http.get<any>(`${environment.apiUrl}/Child/children`);
  }
  addChild(model: any) {
    return this.http.post<any>(`${environment.apiUrl}/Child/child-add`, model);
  }
  childDetails(id: any) {
    return this.http.get<any>(`${environment.apiUrl}/Child/${id}/child-data`);
  }
  childUpdate(id: any, model: any): Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/Child/${id}/child-update`,
      model
    );
  }
  getVaccines() {
    return this.http.get<any>(
      `${environment.apiUrl}/Child/child-base-vaccines`
    );
  }
  deleteChild(childId: any) {
    return this.http.delete<any>(
      `${environment.apiUrl}/Child/${childId}/child-remove`
    );
  }
  addComplaint(model: { message: string }) {
    return this.http.post<any>(
      `${environment.apiUrl}/Parent/complaint-submit`,
      model
    );
  }
  childVaccines() {
    return this.http.get<any>(`${environment.apiUrl}/Child/child-vaccines`);
  }
  deleteChildFile(fileId: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.delete<any>(`${environment.apiUrl}/Child/delete-file`, {
      headers,
      body: { path: fileId },
    });
  }

  parentProfile(id: any) {
    return this.http.get<any>(
      `${environment.apiUrl}/Parent/${id}/user-profile`
    );
  }
  updateParentProfile(model: {
    firstName: string;
    lastName: string;
    phoneNumber: string;
    governorate: string;
    city: string;
    village: string;
    healthCareCenterId: string;
  }) {
    return this.http.put<any>(
      `${environment.apiUrl}/Parent/update-parent`,
      model
    );
  }
  ParentHealthcareDetails() {
    return this.http.get<any>(
      `${environment.apiUrl}/Parent/healthcare-details`
    );
  }
  childVaccineSchedule(childId: any) {
    return this.http.get<any>(`${environment.apiUrl}/Child/${childId}/child`);
  }
  payment(childId: any, model: {}) {
    return this.http.post<any>(
      `${environment.apiUrl}/Payment/${childId}/process-payment`,
      model
    );
  }
  getBookingDates() {
    return this.http.get<any>(
      `${environment.apiUrl}/Parent/healthcare-details`
    );
  }
  appointmentExists(childId: any, vaccineName: any) {
    return this.http.get<any>(
      `${environment.apiUrl}/Parent/${childId}/${vaccineName}/appointment-exists`
    );
  }
  appointmentBook(model: {
    childId: string;
    vaccineName: string;
    day: string;
  }) {
    return this.http.post<any>(
      `${environment.apiUrl}/Parent/appointment-book`,
      model
    );
  }
  appointmentReBook(
    appointmentId: any,
    model: {
      day: string;
    }
  ) {
    return this.http.put<any>(
      `${environment.apiUrl}/Parent/${appointmentId}/appointment-reschedule`,
      model
    );
  }
  appointmentCancel(appointmentId: any) {
    return this.http.delete<any>(
      `${environment.apiUrl}/Parent/${appointmentId}/appointment-cancle`
    );
  }
  childrenReservations() {
    return this.http.get<any>(
      `${environment.apiUrl}/Parent/children-reservations`
    );
  }
}
