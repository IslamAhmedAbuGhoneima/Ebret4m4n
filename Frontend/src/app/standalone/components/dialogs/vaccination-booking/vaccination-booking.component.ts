import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatRadioModule } from '@angular/material/radio';
import { ParentService } from '../../../../features/parent/services/parent.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vaccination-booking',
  imports: [MatRadioModule, FormsModule, CommonModule],
  templateUrl: './vaccination-booking.component.html',
  styleUrl: './vaccination-booking.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class VaccinationBookingComponent implements OnInit {
  errorMessage: any;
  bookingDate: any;
  selectedDay: string = '';

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _ParentService: ParentService,
    private dialog: MatDialogRef<VaccinationBookingComponent>,
    private router: Router
  ) {}
  ngOnInit(): void {
    this._ParentService.getBookingDates().subscribe({
      next: (res) => {
        this.bookingDate = this.formateData(res.data);
      },
      error: (err) => {
        this.errorMessage = err.error.message;
      },
    });
  }

  bookAppointment() {
    if (this.data.bookingExists) {
      const bookingData = {
        day: this.selectedDay,
      };
      this._ParentService
        .appointmentReBook(this.data.appointmentId, bookingData)
        .subscribe({
          next: (res) => {
            this.close();
          },
          error: (err) => {},
        });
    } else {
      const bookingData = {
        childId: this.data.childId,
        vaccineName: this.data.vaccineName,
        day: this.selectedDay,
      };
      this._ParentService.appointmentBook(bookingData).subscribe({
        next: (res) => {
          this.close();
        },
        error: (err) => {},
      });
    }
  }

  formateData(data: any): any {
    if (!data || typeof data !== 'object') {
      return {};
    }

    const dayMap: Record<string, string> = {
      Saturday: 'السبت',
      Sunday: 'الأحد',
      Monday: 'الإثنين',
      Tuesday: 'الثلاثاء',
      Wednesday: 'الأربعاء',
      Thursday: 'الخميس',
      Friday: 'الجمعة',
    };

    return {
      ...data,
      firstDayAr: dayMap[data.firstDay] || data.firstDay,
      secondDayAr: dayMap[data.secondDay] || data.secondDay,
    };
  }
  deleteAppointment() {
    // Swal.fire({
    //   title: 'هل تريد ألغاء الحجز؟',
    //   text: 'سيحذف هذا موعد الحجز الذي أضافته من قبل ',
    //   icon: 'error',
    //   showCancelButton: true,
    //   confirmButtonColor: '#127453',
    //   cancelButtonColor: '#B4231B',
    //   confirmButtonText: 'نعم , حذف',
    //   cancelButtonText: 'لا',
    //   allowOutsideClick: false,
    // }).then((result) => {
    //   if (result.isConfirmed) {
        this._ParentService
          .appointmentCancel(this.data.appointmentId)
          .subscribe({
            next: (res) => {
              this.close();
            },
            error: (err) => {
              this.errorMessage = err.error.message;
            },
          });
      // }
    // });
  }
  close() {
    this.dialog.close();
  }
}
