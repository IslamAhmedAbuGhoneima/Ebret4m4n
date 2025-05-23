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

  searchById(id: any) {
    return this.http.get<any>(
      `${environment.apiUrl}/Organizer/${id}/search_by_id`
    );
  }

  // addCityCenterAdmin(model: any) {
  //   return this.http.post<any>(
  //     `${environment.apiUrl}/GovernorateAdmin/city-admin-add`,
  //     model
  //   );
  // }
}
