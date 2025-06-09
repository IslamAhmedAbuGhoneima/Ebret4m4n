import { Component, OnInit } from '@angular/core';
import { GovernorateAdminService } from '../../../services/governorateAdmin.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-centers-home-page',
  standalone: false,
  templateUrl: './centers-home-page.component.html',
  styleUrl: './centers-home-page.component.css',
})
export class CentersHomePageComponent implements OnInit {
  CitiesCentersList: string[] = [];

  constructor(private _GovernorateAdminService: GovernorateAdminService) {}
  ngOnInit(): void {
    this.getAllCities();
  }

  getAllCities() {
    this._GovernorateAdminService.getCitiesCenters().subscribe({
      next: (res) => {
        this.CitiesCentersList = res.data;
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
