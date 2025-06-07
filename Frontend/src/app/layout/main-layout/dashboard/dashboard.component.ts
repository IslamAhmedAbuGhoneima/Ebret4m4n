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
import { count, filter } from 'rxjs';

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
  unreadCountNotify = 0;
  unreadCountMessages = 0;

  constructor(
    private authService: AuthService,
    private _notificationService: NotificationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // بيانات المستخدم
    this.role = this.authService.getRole();
    this.email = this.authService.getUserEmail();
    this.userName = this.authService.getUserName();
    this.userId = this.authService.getUserId();
    this.loadUnreadCount();

    // setInterval(() => {
    //   this.loadUnreadCount();
    // }, 15000);

    this._notificationService.getNotificationStream().subscribe(() => {
      this.loadUnreadCount();
    });

    this.router.events
      .pipe(
        filter(
          (event): event is NavigationEnd => event instanceof NavigationEnd
        )
      )
      .subscribe((event: NavigationEnd) => {
        if (event.urlAfterRedirects === '/chat') {
          this._notificationService
            .getUnreadCount()
            .subscribe((notifications) => {
              const unreadChatMessages = notifications?.filter(
                (msg: any) => msg.title === 'رساله جديده' && !msg.isRead
              );

              if (unreadChatMessages?.length) {
                let completed = 0;
                unreadChatMessages.forEach((msg: any) => {
                  this._notificationService.markAsRead(msg.id).subscribe(() => {
                    completed++;
                    if (completed === unreadChatMessages.length) {
                      this.loadUnreadCount(); 
                    }
                  });
                });
              }
            });
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
    this._notificationService.getUnreadCount().subscribe((notifications) => {
      const allUnread = notifications?.filter((n: any) => !n.isRead) || [];
      const unreadMessages = allUnread.filter(
        (msg: any) => msg.title === 'رساله جديده'
      );

      this.unreadCountNotify = allUnread.length;
      this.unreadCountMessages = unreadMessages.length;
    });
  }
}
