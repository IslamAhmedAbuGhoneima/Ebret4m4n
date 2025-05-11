import { CanActivateFn, Router } from '@angular/router';
import { PaymentService } from '../services/payment/payment.service';
import { inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PaymentRequiredComponent } from '../../standalone/components/dialogs/payment-required/payment-required.component';

export const paidGuard: CanActivateFn = (route, state) => {
  const paymentService = inject(PaymentService);
  const dialog = inject(MatDialog);

  if (paymentService.isPaid()) {
    return true;
  } else {
    dialog.open(PaymentRequiredComponent, {
      width: '400px',
      disableClose: true,
      panelClass: 'dialog-payment-container',
    });
    return false;
  }
};
