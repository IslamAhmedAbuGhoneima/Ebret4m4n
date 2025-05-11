import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ParentService } from '../../../../features/parent/services/parent.service';
import { Router } from '@angular/router';
import { PaymentService } from '../../../../core/services/payment/payment.service';
import { Location } from '@angular/common';
@Component({
  selector: 'app-payment-required',
  imports: [],
  templateUrl: './payment-required.component.html',
  styleUrl: './payment-required.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class PaymentRequiredComponent {
  errorMessage: any;
  constructor(
    private dialog: MatDialogRef<PaymentRequiredComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private router: Router,
    private paymentService: PaymentService,
    private location: Location
  ) {}

  confirm() {
    this.paymentService.ParentPayment(this.data[1], {}).subscribe({
      next: (res) => {
        const url = res.data;
        if (url) {
          window.open(url, '_blank');
        }
      },
      error: (err) => {
        this.errorMessage = err.error.message;
      },
    });
  }
  ignore() {
    this.dialog.close(false);
  }
}
