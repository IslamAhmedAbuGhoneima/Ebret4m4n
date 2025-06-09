import { Component, OnInit } from '@angular/core';
import { CityCenterService } from '../../../services/cityCenter.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-units-home-page',
  standalone: false,
  templateUrl: './units-home-page.component.html',
  styleUrl: './units-home-page.component.css',
})
export class UnitsHomePageComponent implements OnInit {
  unitList: any[] = [];

  constructor(private _CityCenterService: CityCenterService) {}
  ngOnInit(): void {
    this.getAllCities();
  }

  getAllCities() {
    this._CityCenterService.getHealthCareUnits().subscribe({
      next: (res) => {
        this.unitList = res.data;
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
