import { Component } from '@angular/core';
import { NotificationService } from '../../../../core/services/notification.service';
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
}
