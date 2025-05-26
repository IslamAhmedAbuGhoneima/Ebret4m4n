import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AddHealthCare } from '../../../core/interfaces/addHealthCare';
import { AddAdmin } from '../../../core/interfaces/AddAdmin';

@Injectable({
  providedIn: 'root',
})
export class CityCenterService {
  constructor(private http: HttpClient) {}

  getStatisticsOfCityCenterAdmin() {
    return this.http.get<any>(`${environment.apiUrl}/Statistics/city`);
  }
  getHealthCareUnits() {
    return this.http.get<any>(
      `${environment.apiUrl}/CityAdmin/healthcareCenter-village`
    );
  }
  getOrganizers() {
    return this.http.get<any>(`${environment.apiUrl}/CityAdmin/organizers`);
  }
  addOrganizerOrDoctor(model: AddAdmin) {
    return this.http.post<any>(
      `${environment.apiUrl}/CityAdmin/medical-postion-add`,
      model
    );
  }
  getOrganizerOrDoctorDetails(medicalStaffId: any) {
    return this.http.get<any>(
      `${environment.apiUrl}/CityAdmin/${medicalStaffId}/medicalstaff-data`
    );
  }
  editCityCenterOrganizerOrDoctor(
    medicalStaffId: string,
    model: {
      firstName: string;
      lastName: string;
      email: string;
      healthCareCenterId: string;
    }
  ) {
    return this.http.put<any>(
      `${environment.apiUrl}/CityAdmin/${medicalStaffId}/medicalstaff-update`,
      model
    );
  }
  addHealthCareUnit(model: AddHealthCare) {
    return this.http.post<any>(
      `${environment.apiUrl}/CityAdmin/healthCare-add`,
      model
    );
  }

  getHealthCareUnitDetails(healthUnitId: string) {
    return this.http.get<any>(
      `${environment.apiUrl}/CityAdmin/${healthUnitId}/healthCareCenter`
    );
  }

  getOrders() {
    return this.http.get<any>(
      `${environment.apiUrl}/order/requested-healthcare-orders`
    );
  }
  getComplaints() {
    return this.http.get<any>(`${environment.apiUrl}/CityAdmin/complaints`);
  }
  complaintDetails(id: any) {
    return this.http.get<any>(
      `${environment.apiUrl}/CityAdmin/${id}/complaint`
    );
  }
  handleComplaint(complaintId: any) {
    return this.http.put<any>(
      `${environment.apiUrl}/CityAdmin/${complaintId}/handle-complaint`,
      {}
    );
  }

  getDoctors() {
    return this.http.get<any>(`${environment.apiUrl}/CityAdmin/doctors`);
  }
  acceptOrders(orderId: string) {
    return this.http.put<any>(
      `${environment.apiUrl}/Order/${orderId}/orgnizer-recived-order`,
      {}
    );
  }
}
