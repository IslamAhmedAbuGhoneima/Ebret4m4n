import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, map } from 'rxjs';
import { AuthService } from '../../features/auth/services/auth.service';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { ToastrService } from 'ngx-toastr';
@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private hubConnection: signalR.HubConnection;
  private notificationReceived$ = new Subject<any>();

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private toastr: ToastrService
  ) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5112/notification', {
        accessTokenFactory: () => this.authService.getToken() || '',
      })
      .withAutomaticReconnect()
      .build();
      
    this.hubConnection.on('NotificationMessage', (data) => {
      console.log(`from On Metjod `,data);
      this.notificationReceived$.next(data);
      
      if (data && data.message && data.title) {
        this.toastr.success(data.message, data.title, {
          timeOut: 5000,
          positionClass: 'toast-top-left',
          closeButton: true,
          progressBar: true
        });
      } else {
        console.error('Invalid notification data:', data);
      }
    });

    this.startConnection();
  }
  public getNotificationStream() {
    return this.notificationReceived$.asObservable();
  }
  private async startConnection() {
    try {
      await this.hubConnection.start();
    } catch (err) {
      console.error('Notification SignalR Error:', err);
      setTimeout(() => this.startConnection(), 5000);
    }
  }

  requestPermission(): void {
    if ('Notification' in window) {
      Notification.requestPermission().then((permission) => {});
    }
  }

  showNotification(title: string, body: string): void {
    if ('Notification' in window && Notification.permission === 'granted') {
      new Notification(title, {
        body: body,
        icon: 'assets/icons/notification.png',
      });
    }
  }
  getUnreadCount(): Observable<any> {
    return this.http
      .get<any>(`${environment.apiUrl}/Notification/notifications`)
      .pipe(map((response) => response.data));
  }
  getNotifications(): Observable<Notification[]> {
    return this.http.get<Notification[]>(
      `${environment.apiUrl}/Notification/notifications`
    );
  }

  markAsRead(id: string): Observable<void> {
    return this.http.post<void>(
      `${environment.apiUrl}/Notification/${id}/mark-as-read`,
      {}
    );
  }
}
