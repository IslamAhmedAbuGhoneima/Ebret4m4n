import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  private hasPaid = false;
  constructor(protected http: HttpClient) {}

  setPaymentStatus(status: boolean) {
    this.hasPaid = status;
  }

  isPaid(): boolean {
    return this.hasPaid;
  }
  ParentPayment(childId: any, model: {}) {
    return this.http.post<any>(
      `${environment.apiUrl}/Payment/${childId}/process-payment`,
      model
    );
  }
}
