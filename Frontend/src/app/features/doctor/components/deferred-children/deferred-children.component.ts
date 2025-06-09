import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../../services/doctor.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-deferred-children',
  standalone: false,
  templateUrl: './deferred-children.component.html',
  styleUrl: './deferred-children.component.css',
})
export class DeferredChildrenComponent implements OnInit {
  searchId: any;
  filteredData: any;
  data: any;
  constructor(private router: Router, private _DoctorService: DoctorService) {}

  ngOnInit(): void {
    this.getChildrenSuspended();
  }

  getChildrenSuspended() {
    this._DoctorService.childrenSuspended().subscribe({
      next: (res) => {
        this.data = res.data;
        this.filteredData = this.data;
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
  onSearch() {
    const id = this.searchId.trim();
    if (id) {
      this.filteredData = this.data.filter((child: any) =>
        child.childId?.toString().includes(id)
      );
    } else {
      this.filteredData = this.data;
    }
  }
}
