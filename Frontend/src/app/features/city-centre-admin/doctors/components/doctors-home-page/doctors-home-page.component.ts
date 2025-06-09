import { Component, OnInit } from '@angular/core';
import { CityCenterService } from '../../../services/cityCenter.service';
import { AuthService } from '../../../../auth/services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-doctors-home-page',
  standalone: false,
  templateUrl: './doctors-home-page.component.html',
  styleUrl: './doctors-home-page.component.css',
})
export class DoctorsHomePageComponent implements OnInit {
  role: any;
  data: any[] = [];
  filteredData: any[] = [];
  searchTerm: string = '';
  constructor(
    private authService: AuthService,
    private _CityCenterService: CityCenterService
  ) {}

  ngOnInit(): void {
    this.role = this.authService.getRole()!;
    this.getDoctors();
  }

  getDoctors() {
    this._CityCenterService.getDoctors().subscribe({
      next: (res: { data: any }) => {
        this.data = res.data;
        this.filteredData = res.data;
      },
      error: (error: any) => {
      const containsNonArabic =
        /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(error.error.message);

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
  normalize(text: string): string {
    return text
      .toLowerCase()
      .replace(/[أإآ]/g, 'ا')
      .replace(/ال/g, '')
      .replace(/\s+/g, '') // إزالة المسافات
      .trim();
  }

  search(term: string) {
    const normalizedTerm = this.normalize(term);

    if (!normalizedTerm) {
      this.filteredData = this.data;
      return;
    }

    this.filteredData = this.data.filter((item) => {
      const fullName = `${item.firstName || ''} ${item.lastName || ''}`;
      const normalizedFullName = this.normalize(fullName);

      return normalizedFullName.includes(normalizedTerm);
    });
  }
}
