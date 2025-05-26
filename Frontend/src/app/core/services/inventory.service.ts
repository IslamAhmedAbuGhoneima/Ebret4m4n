import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  constructor(private http: HttpClient) {}
  //govern,city
  adminCreateInventory(model: any) {
    return this.http.post<any>(
      `${environment.apiUrl}/Inventory/admins-create-inventory`,
      model
    );
  }
  getAdminInventory() {
    return this.http.get<any>(`${environment.apiUrl}/Inventory/inventory`);
  }
  vaccineOrder(model: any) {
    return this.http.post<any>(
      `${environment.apiUrl}/Order/request-order`,
      model
    );
  }
  //organizer
  organizerCreateInventory(model: any) {
    return this.http.post<any>(
      `${environment.apiUrl}/Inventory/organizer-create-inventory`,
      model
    );
  }
  getOrganizerInventory() {
    return this.http.get<any>(`${environment.apiUrl}/Inventory/get_orgnizer_inventory`);
  }
}
