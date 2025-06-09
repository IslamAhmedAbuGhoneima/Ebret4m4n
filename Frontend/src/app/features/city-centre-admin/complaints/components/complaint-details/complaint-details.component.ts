import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { CityCenterService } from '../../../services/cityCenter.service';
import { AuthService } from '../../../../auth/services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-complaint-details',
  standalone: false,
  templateUrl: './complaint-details.component.html',
  styleUrl: './complaint-details.component.css',
})
export class ComplaintDetailsComponent implements OnInit {
  complaintId: any;
  data: any;
  organizerGovernorateName: any;
  organizerCityName: any;
  organizerEmail: any;

  constructor(
    private _ActivatedRoute: ActivatedRoute,
    private location: Location,
    private _CityCenterService: CityCenterService,
    private _AuthService: AuthService,
    private route: Router
  ) {}

  ngOnInit(): void {
    this.organizerGovernorateName = this._AuthService.getUserGovernorate()!;
    this.organizerCityName = this._AuthService.getUserCity()!;
    this.organizerEmail = this._AuthService.getUserEmail()!;
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.complaintId = params.get('id');
      this.getComplaintDetails();
    });
  }
  getComplaintDetails() {
    this._CityCenterService.complaintDetails(this.complaintId).subscribe({
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
  handleComplaint() {
    this._CityCenterService.handleComplaint(this.complaintId).subscribe({
      next: (res) => {
        Swal.fire({
          title: res.data,
          text: 'تم حل الشكوى بنجاح وإبلاغ ولي الأمر. يُرجى متابعة أي ردود لاحقة إن وُجدت.',
          icon: 'success',
          showCancelButton: false,
          showConfirmButton: true,
          confirmButtonColor: '#127453',
          cancelButtonColor: '#127453',
          confirmButtonText: 'إغلاق',
          allowOutsideClick: false,
        }).then((result) => {
          if (result.isConfirmed) {
            this.route.navigate(['/city-center-admin/complaints']);
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
    this.route.navigate(['/city-center-admin/complaints']);
  }
}
