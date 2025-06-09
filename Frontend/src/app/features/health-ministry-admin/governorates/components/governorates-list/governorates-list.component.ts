import { Component, OnInit } from '@angular/core';
import { HealthMinistryService } from '../../../services/health-ministry.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-governorates-list',
  standalone: false,
  templateUrl: './governorates-list.component.html',
  styleUrl: './governorates-list.component.css',
})
export class GovernoratesListComponent implements OnInit {
  governoratesList: string[] = [];

  constructor(private _HealthMinistryService: HealthMinistryService) {}
  ngOnInit(): void {
    this.getAllCities();
  }

  getAllCities() {
    this._HealthMinistryService.getGovernorates().subscribe({
      next: (res) => {
        this.governoratesList = res.data;
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
