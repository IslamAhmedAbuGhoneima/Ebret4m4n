import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { HealthMinistryService } from '../../../../features/health-ministry-admin/services/health-ministry.service';
import { GovernorateAdminService } from '../../../../features/city-admin/services/governorateAdmin.service';
import { FormsModule } from '@angular/forms';
import { CityCenterService } from '../../../../features/city-centre-admin/services/cityCenter.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-administrators',
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './administrators.component.html',
  styleUrl: './administrators.component.css',
})
export class AdministratorsComponent implements OnInit {
  role: any;
  data: any[] = [];
  filteredData: any[] = [];
  searchTerm: string = '';
  constructor(
    private authService: AuthService,
    private _HealthMinistryService: HealthMinistryService,
    private _GovernorateAdminService: GovernorateAdminService,
    private _CityCenterService: CityCenterService
  ) {}

  ngOnInit(): void {
    this.role = this.authService.getRole()!;
    this.getAdmins();
  }

  //all function to health ministry
  getAdmins() {
    if (this.role === 'admin') {
      this._HealthMinistryService.getGovernoratesAdmins().subscribe({
        next: (res: { data: any }) => {
          this.data = res.data;
          this.filteredData = res.data;
        },
        error: (error: any) => {
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
    } else if (this.role === 'governorateAdmin') {
      this._GovernorateAdminService.getCitiesCenterAdmins().subscribe({
        next: (res) => {
          this.data = res.data;
          this.filteredData = res.data;
        },
        error: (error: any) => {
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
    } else if (this.role === 'cityAdmin') {
      this._CityCenterService.getOrganizers().subscribe({
        next: (res) => {
          this.data = res.data;
          this.filteredData = res.data;
        },
        error: (error: any) => {
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
      let targetField = '';

      if (this.role === 'admin') {
        targetField = this.normalize(item.governorate || '');
      } else if (this.role === 'governorateAdmin') {
        targetField = this.normalize(item.city || '');
      }

      return targetField.includes(normalizedTerm);
    });
  }
}
