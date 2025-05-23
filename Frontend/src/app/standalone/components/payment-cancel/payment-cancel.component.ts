import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-payment-cancel',
  imports: [],
  templateUrl: './payment-cancel.component.html',
  styleUrl: './payment-cancel.component.css',
})
export class PaymentCancelComponent {
  constructor(private loc: Location) {}
  paymentPage() {
    this.loc.back();
  }
}
