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
import { trigger } from '@angular/animations';

@Component({
  selector: 'app-vaccination-booking',
  imports: [MatRadioModule, FormsModule, CommonModule],
  templateUrl: './vaccination-booking.component.html',
  styleUrl: './vaccination-booking.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class VaccinationBookingComponent implements OnInit {
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

  bookAppointment() {
    if (this.data.bookingExists) {
      const bookingData = {
        day: this.selectedDay,
      };
      this._ParentService
        .appointmentReBook(this.data.appointmentId, bookingData)
        .subscribe({
          next: (res) => {
            Swal.fire({
              title: `تم تعديل  موعد التطعيم لطفلك  <br/> ${this.data.childName} بنجاح `,
              text: 'ننتظرك في الوحده الصحيه الاسبوع المقبل في اليوم الذي اخترته',
              icon: 'success',
              showCancelButton: true,
              showConfirmButton: false,
              confirmButtonColor: '#127453',
              cancelButtonColor: '#127453',
              cancelButtonText: 'حسناً , إغلاق',
              allowOutsideClick: false,
            });
            this.close();
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
    } else {
      const bookingData = {
        childId: this.data.childId,
        vaccineName: this.data.vaccineName,
        day: this.selectedDay,
      };
      this._ParentService.appointmentBook(bookingData).subscribe({
        next: (res) => {
          Swal.fire({
            title: `تم حجز موعد التطعيم  لطفلك  <br/> ${this.data.childName} بنجاح `,
            text: 'ننتظرك في الوحده الصحيه الاسبوع المقبل في اليوم الذي اخترته',
            icon: 'success',
            showCancelButton: true,
            showConfirmButton: false,
            confirmButtonColor: '#127453',
            cancelButtonColor: '#127453',
            cancelButtonText: 'حسناً , إغلاق',
            allowOutsideClick: false,
          });
          this.close();
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
    Swal.fire({
      title: `هل تريد ألغاء موعد تطعيم  <br/> طفلك  ${this.data.childName} ؟`,
      text: ``,
      icon: 'error',
      showCancelButton: true,
      confirmButtonColor: '#127453',
      cancelButtonColor: '#B4231B',
      confirmButtonText: 'نعم',
      cancelButtonText: 'لا',
      allowOutsideClick: false,
    }).then((result) => {
      if (result.isConfirmed) {
        this._ParentService
          .appointmentCancel(this.data.appointmentId)
          .subscribe({
            next: (res) => {
              Swal.close();
              this.close();
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
      } else if (result.dismiss) {
        this.close();
      }
    });
  }
  close() {
    this.dialog.close();
  }
}
