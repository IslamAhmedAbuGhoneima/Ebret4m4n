import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { ParentService } from '../../../../features/parent/services/parent.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-payment-required',
  imports: [],
  templateUrl: './payment-required.component.html',
  styleUrl: './payment-required.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class PaymentRequiredComponent implements OnInit {
  errorMessage: any;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog: MatDialogRef<PaymentRequiredComponent>,
    private router: Router,
    private _ParentService: ParentService
  ) {}
  ngOnInit(): void {}

  confirm() {
    this._ParentService.payment(this.data, {}).subscribe({
      next: (res) => {
        const url = res.data;
        if (url) {
          window.open(url, '_self');
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
