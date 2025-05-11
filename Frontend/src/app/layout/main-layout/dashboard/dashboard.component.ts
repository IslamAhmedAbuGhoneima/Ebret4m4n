import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AuthService } from '../../../features/auth/services/auth.service';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink, RouterLinkActive, CommonModule, MatTooltipModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class DashboardComponent implements OnInit {
  role: string | null = null;
  email: string | null = null;
  userName: string | null = null;
  userId:any

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.role = this.authService.getRole();
    this.email = this.authService.getUserEmail();
    this.userName = this.authService.getUserName();
    this.userId = this.authService.getUserId();
  }

  hasRole(allowedRoles: string[]): boolean {
    return this.role !== null && allowedRoles.includes(this.role);
  }
  logOut() {
    this.authService.logout();
  }
}
