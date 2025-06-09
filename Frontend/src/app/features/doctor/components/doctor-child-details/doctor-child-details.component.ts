import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { DoctorService } from '../../services/doctor.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-doctor-child-details',
  standalone: false,
  templateUrl: './doctor-child-details.component.html',
  styleUrl: './doctor-child-details.component.css',
})
export class DoctorChildDetailsComponent implements OnInit {
  fromPage: any;
  deferredChild: boolean = true;
  childId: any;
  data: any;
  medicalImagesFromApi: any;
  constructor(
    private _ActivatedRoute: ActivatedRoute,
    private router: Router,
    private _DoctorService: DoctorService
  ) {}

  ngOnInit(): void {
    this._ActivatedRoute.queryParams.subscribe((params) => {
      this.fromPage = params['from'];
    });
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.childId = params.get('id');
      this.childDetails();
    });
  }
  childDetails() {
    this._DoctorService.childDiseaseDetails(this.childId).subscribe({
      next: (res) => {
        this.data = res.data;
        this.medicalImagesFromApi = [];
        this.data.filePath.forEach((path: string) => {
          const fullUrl = 'http://localhost:5112' + path;
          const extension = path.split('.').pop()?.toLowerCase();
          const isImage = ['jpg', 'jpeg', 'png', 'gif', 'bmp', 'webp'].includes(
            extension || ''
          );

          const fileObj = isImage
            ? { preview: fullUrl, type: 'image', path: path }
            : {
                name: path.split('/').pop(),
                preview: fullUrl,
                type: 'file',
                path: path,
              };

          this.medicalImagesFromApi.push(fileObj);
        });
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
  vaccinationPostponement() {
    this._DoctorService.childSuspended(this.childId).subscribe({
      next: (res) => {
        Swal.fire({
          title: res.data,
          text: 'سيتم إبلاغ ولي الأمر بالموعد المُعدّل.',
          icon: 'success',
          showCancelButton: true,
          showConfirmButton: false,
          confirmButtonColor: '#127453',
          cancelButtonColor: '#127453',
          cancelButtonText: 'حسناً , إغلاق',
          allowOutsideClick: false,
        }).then((result) => {
          if (result.dismiss) {
            if (this.fromPage == 'children') {
              this.router.navigate(['/doctor/children']);
            } else if (this.fromPage == 'deferred') {
              this.router.navigate(['/doctor/deferred-children']);
            } else {
              this.router.navigate(['/doctor']);
            }
          }
        });
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
  allowVaccination() {
    this._DoctorService.acceptVaccination(this.childId).subscribe({
      next: (res) => {
        Swal.fire({
          title: res.data,
          text: '',
          icon: 'success',
          showCancelButton: true,
          showConfirmButton: false,
          confirmButtonColor: '#127453',
          cancelButtonColor: '#127453',
          cancelButtonText: 'حسناً , إغلاق',
          allowOutsideClick: false,
        }).then((result) => {
          if (result.dismiss) {
            if (this.fromPage == 'children') {
              this.router.navigate(['/doctor/children']);
            } else if (this.fromPage == 'deferred') {
              this.router.navigate(['/doctor/deferred-children']);
            } else {
              this.router.navigate(['/doctor']);
            }
          }
        });
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
  goBack() {
    if (this.fromPage == 'children') {
      this.router.navigate(['/doctor/children']);
    } else if (this.fromPage == 'deferred') {
      this.router.navigate(['/doctor/deferred-children']);
    } else {
      this.router.navigate(['/doctor']);
    }
  }
}
