import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class OrganizerService {
  constructor(private http: HttpClient) {}

  getChildren() {
    return this.http.get<any>(
      `${environment.apiUrl}/Organizer/coming_children`
    );
  }

  updateVaccineStatues(body: { id: string; vaccineName: string }[]) {
    return this.http.post<any>(
      `${environment.apiUrl}/Organizer/update_vaccine_statues`,
      body
    );
  }

  postponeChildAppointmentVaccine(id: any) {
    return this.http.post<any>(
      `${environment.apiUrl}/Organizer/${id}/postpone_child_appointment_vaccine`,
      {}
    );
  }
}
