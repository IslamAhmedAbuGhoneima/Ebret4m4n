import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ParentService } from '../../../services/parent.service';
import { VaccinationBookingComponent } from '../../../../../standalone/components/dialogs/vaccination-booking/vaccination-booking.component';
import { elements } from 'chart.js';

@Component({
  selector: 'app-parent-home-page',
  standalone: false,
  templateUrl: './parent-home-page.component.html',
  styleUrl: './parent-home-page.component.css',
})
export class ParentHomePageComponent implements OnInit {
  data: any;
  errorMessage: string = '';
  constructor(
    private matDialog: MatDialog,
    private _ParentService: ParentService
  ) {}
  ngOnInit(): void {
    this.getReservations();
  }

  editVaccine(element: any) {
    (document.activeElement as HTMLElement)?.blur();
    setTimeout(() => {
      const dialogRef = this.matDialog.open(VaccinationBookingComponent, {
        width: '520px',
        panelClass: 'dialog-vaccination-booking-container',
        autoFocus: true,
        restoreFocus: false,
        disableClose: true,
        data: {
          vaccineName: element.vaccineName,
          bookingExists: true,
          appointmentId: element.id,
        },
      });
    }, 0);
  }
  getReservations() {
    this._ParentService.childrenReservations().subscribe({
      next: (res) => {
        this.data = this.formateData(res.data);
      },
      error: (err) => {
        this.errorMessage = err.error.message;
      },
    });
  }

  formateData(dataArray: any[]): any[] {
    if (!Array.isArray(dataArray)) {
      return [];
    }

    return dataArray.map((item) => {
      let dayAr = item.day;

      if (item.day == 'Saturday') {
        dayAr = 'السبت';
      } else if (item.day == 'Sunday') {
        dayAr = 'الأحد';
      } else if (item.day == 'Monday') {
        dayAr = 'الإثنين';
      } else if (item.day == 'Tuesday') {
        dayAr = 'الثلاثاء';
      } else if (item.day == 'Wednesday') {
        dayAr = 'الأربعاء';
      } else if (item.day == 'Thursday') {
        dayAr = 'الخميس';
      } else if (item.day == 'Friday') {
        dayAr = 'الجمعة';
      }

      return {
        ...item,
        dayAr: dayAr,
      };
    });
  }
}
