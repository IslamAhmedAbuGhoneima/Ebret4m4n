import { Component, OnInit } from '@angular/core';
import { CityCenterService } from '../../../services/cityCenter.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-complaints-list',
  standalone: false,
  templateUrl: './complaints-list.component.html',
  styleUrl: './complaints-list.component.css',
})
export class ComplaintsListComponent implements OnInit {
  complaintsList: any[] = [];

  constructor(private _CityCenterService: CityCenterService) {}
  ngOnInit(): void {
    this.getAllCities();
  }

  getAllCities() {
    this._CityCenterService.getComplaints().subscribe({
      next: (res) => {
        this.complaintsList = res.data;
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
