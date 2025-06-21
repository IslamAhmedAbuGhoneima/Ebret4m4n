import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ParentService } from '../../../services/parent.service';
import { VaccinationBookingComponent } from '../../../../../standalone/components/dialogs/vaccination-booking/vaccination-booking.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-parent-home-page',
  standalone: false,
  templateUrl: './parent-home-page.component.html',
  styleUrl: './parent-home-page.component.css',
})
export class ParentHomePageComponent implements OnInit {
  data: any;

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
      const dialogRef = this.matDialog
        .open(VaccinationBookingComponent, {
          width: '520px',
          panelClass: 'dialog-vaccination-booking-container',
          autoFocus: true,
          restoreFocus: false,
          disableClose: true,
          data: {
            vaccineName: element.vaccineName,
            bookingExists: true,
            appointmentId: element.id,
            childName: element.childName,
          },
        })
        .afterClosed()
        .subscribe((result) => {
          this.getReservations();
        });
    }, 0);
  }
  getReservations() {
    this._ParentService.childrenReservations().subscribe({
      next: (res) => {
        this.data = this.formateData(res.data);
      },
      error: (error) => {
        const containsNonArabic =
          /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(
            error.error.message
          );

        const finalMessage = containsNonArabic
          ? `يوجد مشكلة مؤقتة في النظام. نعتذر عن الإزعاج، 
     
       الرجاء إعادة المحاولة بعد قليل.`
          : error.error.message;

        Swal.fire({
          icon: 'error',
          title: 'عذراً، حدث خطأ',
          text: finalMessage,
          confirmButtonColor: '#127453',
          confirmButtonText: 'حسناً , إغلاق',
        });
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
