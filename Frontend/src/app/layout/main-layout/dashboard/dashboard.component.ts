import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {
  NavigationEnd,
  Router,
  RouterLink,
  RouterLinkActive,
} from '@angular/router';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AuthService } from '../../../features/auth/services/auth.service';
import { NotificationService } from '../../../core/services/notification.service';
import { count } from 'rxjs';

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
  userId: any;
  unreadCount = 0;

  constructor(
    private authService: AuthService,
    private _notificationService: NotificationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.role = this.authService.getRole();
    this.email = this.authService.getUserEmail();
    this.userName = this.authService.getUserName();
    this.userId = this.authService.getUserId();
    this.loadUnreadCount();

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        if (
          event.urlAfterRedirects === '/notifications' &&
          !this.hasVisitedNotifications
        ) {
          this.hasVisitedNotifications = true;
          this.unreadCount = 0;
        }
      }
    });
  }

  hasRole(allowedRoles: string[]): boolean {
    return this.role !== null && allowedRoles.includes(this.role);
  }
  logOut() {
    this.authService.logout();
  }
  loadUnreadCount() {
    this.unreadCount = 0;

    // لو دخل قبل كده على صفحة الإشعارات، منجبش عدد غير المقروء
    if (this.hasVisitedNotifications) return;

    this._notificationService.getUnreadCount().subscribe((notifications) => {
      notifications?.forEach((element: any) => {
        if (!element.isRead) {
          this.unreadCount += 1;
        }
      });
    });
  }

  get hasVisitedNotifications(): boolean {
    return localStorage.getItem('hasVisitedNotifications') === 'true';
  }
  set hasVisitedNotifications(value: boolean) {
    localStorage.setItem('hasVisitedNotifications', value.toString());
  }
}
