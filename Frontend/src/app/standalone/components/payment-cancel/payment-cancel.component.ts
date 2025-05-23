import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment-cancel',
  imports: [],
  templateUrl: './payment-cancel.component.html',
  styleUrl: './payment-cancel.component.css',
})
export class PaymentCancelComponent {
  constructor(private router: Router) {}
  back() {
    this.router.navigate(['/parent/my-children/child-vaccine-schedule']);
  }
}
