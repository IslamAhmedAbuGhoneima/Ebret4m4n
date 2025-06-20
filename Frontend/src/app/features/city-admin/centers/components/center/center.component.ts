import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { GovernorateAdminService } from '../../../services/governorateAdmin.service';
import { AuthService } from '../../../../auth/services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-center',
  standalone: false,
  templateUrl: './center.component.html',
  styleUrl: './center.component.css',
})
export class CenterComponent implements OnInit {
  data: any;
  cityName: any;
  governorate: any;
  constructor(
    private _ActivatedRoute: ActivatedRoute,
    private _GovernorateAdminService: GovernorateAdminService,
    private router: Router,
    private _AuthService: AuthService
  ) {}

  ngOnInit(): void {
    this.governorate = this._AuthService.getUserGovernorate();
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.cityName = params.get('cityName')!;
      if (this.cityName) this.GovernorateDetails();
    });
  }
  GovernorateDetails() {
    this._GovernorateAdminService
      .getCityCenterDetails(this.cityName)
      .subscribe({
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

  goBack() {
    this.router.navigate(['/city-admin/centers']);
  }
}
