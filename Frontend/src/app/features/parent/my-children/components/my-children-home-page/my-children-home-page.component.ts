import { Component, ViewEncapsulation } from '@angular/core';
import { ParentService } from '../../../services/parent.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { PaymentRequiredComponent } from '../../../../../standalone/components/dialogs/payment-required/payment-required.component';
import Swal from 'sweetalert2';

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
    Swal.fire({
      title: 'هل تريد حذف هذا الطفل ؟',
      text: '',
      icon: 'error',
      showCancelButton: true,
      confirmButtonColor: '#127453',
      cancelButtonColor: '#B4231B',
      confirmButtonText: 'نعم',
      cancelButtonText: 'لا',
      allowOutsideClick: false,
    }).then((result) => {
      if (result.isConfirmed) {
        this._ParentService.deleteChild(id).subscribe({
          next: (res) => {
            this.getAllChildren();
            Swal.close();
          },
          error: (err) => {},
        });
      }
    });
  }

  onNavigateToVaccineSchedule(id: any) {
    const childId = id;
    (document.activeElement as HTMLElement)?.blur();
    this._ParentService.childVaccineSchedule(childId).subscribe({
      next: (res) => {
        this.router.navigate([
          '/parent/my-children/child-vaccine-schedule',
          childId,
        ]);
      },
      error: (error) => {



        // console.log(error.error.message);
        // this.matDialog.open(PaymentRequiredComponent, {
        //   width: '400px',
        //   disableClose: true,
        //   data: childId,
        //   panelClass: 'dialog-payment-container',
        // });


        
      },
    });
  }
}
