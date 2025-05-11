import { Component, ViewEncapsulation } from '@angular/core';
import { ParentService } from '../../../services/parent.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmComponent } from '../../../../../standalone/components/dialogs/confirm/confirm.component';
import { PaymentService } from '../../../../../core/services/payment/payment.service';
import { Router } from '@angular/router';
import { PaymentRequiredComponent } from '../../../../../standalone/components/dialogs/payment-required/payment-required.component';

@Component({
  selector: 'app-my-children-home-page',
  standalone: false,
  templateUrl: './my-children-home-page.component.html',
  styleUrl: './my-children-home-page.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class MyChildrenHomePageComponent {
  data: any;
  errorMessage: string = '';
  constructor(
    private _ParentService: ParentService,
    private matDialog: MatDialog,
    private paymentService: PaymentService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.getAllChildren();
  }

  getAllChildren() {
    this._ParentService.getMyChildren().subscribe({
      next: (res) => {
        this.data = res.data;
      },
      error: (err) => {
        this.errorMessage = err.error.message;
      },
    });
  }
  deleteChild(id: any) {
    (document.activeElement as HTMLElement)?.blur();
    setTimeout(() => {
      const dialogRef = this.matDialog
        .open(ConfirmComponent, {
          width: '690px',
          panelClass: 'dialog-delete-container',
          autoFocus: true,
          restoreFocus: false,
          disableClose: true,
          data: [id],
        })
        .afterClosed()
        .subscribe((confirmed) => {
          if (confirmed) {
            this.getAllChildren();
          }
        });
    }, 0);
  }
  onNavigateToVaccineSchedule(name: string, id: string) {
    (document.activeElement as HTMLElement)?.blur();

    if (this.paymentService.isPaid()) {
      this.router.navigate([
        '/parent/my-children/child-vaccine-schedule',
        name,
        id,
      ]);
    } else {
      this.matDialog.open(PaymentRequiredComponent, {
        width: '400px',
        disableClose: true,
        data: [name, id],
        panelClass: 'dialog-payment-container',
      });
    }
  }
}
