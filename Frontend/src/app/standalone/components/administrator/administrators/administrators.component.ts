import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { HealthMinistryService } from '../../../../features/health-ministry-admin/services/health-ministry.service';
import { GovernorateAdminService } from '../../../../features/city-admin/services/governorateAdmin.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-administrators',
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './administrators.component.html',
  styleUrl: './administrators.component.css',
})
export class AdministratorsComponent {
  role: any;
  data: any[] = [];
  filteredData: any[] = [];
  searchTerm: string = '';
  constructor(
    private authService: AuthService,
    private _HealthMinistryService: HealthMinistryService,
    private _GovernorateAdminService: GovernorateAdminService
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
        error: (err: any) => {
          console.log(err);
        },
      });
    } else if (this.role === 'governorateAdmin') {
      this._GovernorateAdminService.getCitiesCenterAdmins().subscribe({
        next: (res) => {
          this.data = res.data;
          this.filteredData = res.data;
        },
        error: (err: any) => {
          console.log(err);
        },
      });
    }
  }
  normalize(text: string): string {
    return text
      .toLowerCase()
      .replace(/ال/g, '') // إزالة "ال"
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
