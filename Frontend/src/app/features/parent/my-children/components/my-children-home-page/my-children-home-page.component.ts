import { Component, ViewEncapsulation } from '@angular/core';
import { ParentService } from '../../../services/parent.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
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
        const errorNumber = error.error?.errorNumber;
        const message = error.error?.message;
        if (errorNumber === 1 || errorNumber === 2) {
          Swal.fire({
            title: message,
            text: 'أذهب للتواصل مع طبيبك للمراجعه',
            icon: 'info',
            showCancelButton: true,
            showConfirmButton: true,
            confirmButtonColor: '#127453',
            cancelButtonColor: '#B4231B',
            confirmButtonText: 'تواصل',
            cancelButtonText: 'إغلاق',
            allowOutsideClick: false,
          }).then((result) => {
            if (result.isConfirmed) {
              this.router.navigate(['/chat']);
            }
          });
        } else if (errorNumber === 3) {
          Swal.fire({
            title: message,
            text: 'الرجاء الدفع لأستخدام هذه الميزة',
            icon: 'info',
            showCancelButton: true,
            showConfirmButton: true,
            confirmButtonColor: '#127453',
            cancelButtonColor: '#B4231B',
            confirmButtonText: 'دفع',
            cancelButtonText: 'إغلاق',
            allowOutsideClick: false,
          }).then((result) => {
            if (result.isConfirmed) {
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
            } else if (result.dismiss) {
              Swal.close();
            }
          });
        } else {
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
        }
      },
    });
  }
}
