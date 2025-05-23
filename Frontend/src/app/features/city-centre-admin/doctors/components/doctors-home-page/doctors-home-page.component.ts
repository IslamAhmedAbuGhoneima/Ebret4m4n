import { Component, OnInit } from '@angular/core';
import { CityCenterService } from '../../../services/cityCenter.service';
import { AuthService } from '../../../../auth/services/auth.service';

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
      error: (err: any) => {
        console.log(err);
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
