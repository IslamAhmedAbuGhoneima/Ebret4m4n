import { Component } from '@angular/core';
import { NotificationService } from '../../../../core/services/notification.service';
import { Notification } from '../../../../core/interfaces/Notification';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-notifications',
  imports: [CommonModule, RouterModule],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.css',
})
export class NotificationsComponent {
  notifications: any;
  unreadCount = 0;
  showDropdown = false;

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    if (!localStorage.getItem('hasVisitedNotifications')) {
      localStorage.setItem('hasVisitedNotifications', 'true');
    }
    this.notificationService.getNotifications().subscribe((n: any) => {
      this.notifications = n.data;
    });

    this.notificationService.getNotificationStream().subscribe((newNoti) => {
      this.notifications.unshift(newNoti);
    });
  }

  markAsRead(id: any) {
    this.notificationService.markAsRead(id).subscribe(() => {
      this.notificationService.getNotifications().subscribe((n: any) => {
        this.notifications = n.data;
      });
    });
  }

  private updateUnreadCount() {
    this.unreadCount = this.notifications.filter(
      (n: Notification) => !n.isRead
    ).length;
  }
}
