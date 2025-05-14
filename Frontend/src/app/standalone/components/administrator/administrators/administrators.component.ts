import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { GlobalService } from '../../../../core/services/APIs/global.service';
import { HealthMinistryService } from '../../../../features/health-ministry-admin/services/health-ministry.service';

@Component({
  selector: 'app-administrators',
  imports: [CommonModule, RouterLink],
  templateUrl: './administrators.component.html',
  styleUrl: './administrators.component.css',
})
export class AdministratorsComponent {
  role: string | null = null;
  data: any;
  constructor(
    private authService: AuthService,
    private _HealthMinistryService: HealthMinistryService
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
        },
        error: (err: any) => {
          console.log(err);
        },
      });
    }
  }
}
