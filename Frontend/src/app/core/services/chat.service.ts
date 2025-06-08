import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Message } from '../interfaces/message';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../features/auth/services/auth.service';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private hubConnection: signalR.HubConnection;
  private messageReceived$ = new Subject<any>();
  private currentUserId: string | null = null;
  private messageDeleted$ = new Subject<any>();
  private messagesRead$ = new Subject<any>();

  constructor(private authService: AuthService, private http: HttpClient) {
    this.currentUserId = this.authService.getUserId();

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5112/chat', {
        accessTokenFactory: () => {
          const token = this.authService.getToken();
          return token || '';
        },
      })
      .withAutomaticReconnect()
      .build();

    // Add connection state logging
    this.hubConnection.onreconnecting((error) => {
      console.log('SignalR Reconnecting:', error);
    });

    this.hubConnection.onreconnected((connectionId) => {
      console.log('SignalR Reconnected:', connectionId);
    });

    this.hubConnection.onclose((error) => {
      console.log('SignalR Connection Closed:', error);
    });

    // Add message received handler with duplicate prevention
    this.hubConnection.on('ReceiveMessage', (data) => {
      // Only process the message if we're the receiver
      if (data.receiverId === this.currentUserId) {
        this.messageReceived$.next(data);
      } else if (data.senderId === this.currentUserId) {
        // If we're the sender, just log it but don't emit
      }
    });

    this.hubConnection.on('MessageDeleted', (messageId: any) => {
      this.messageDeleted$.next(messageId);
    });
    this.hubConnection.on('MessagesMarkedAsRead', (data) => {
      this.messagesRead$.next(data);
    });
    // Start the connection
    this.startConnection();
  }

  //parent
  getMessages(receiverId: string): Observable<Message[]> {
    return this.http.get<Message[]>(
      `${environment.apiUrl}/Chat/${receiverId}/user-messages`
    );
  }

  getHealthcareDoctorId(): Observable<string> {
    return this.http.get<string>(
      `${environment.apiUrl}/Parent/get-healthcare-doctor-id`
    );
  }
  //doctor
  getUserChatList(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}/Chat/user-chat-list`);
  }

  private async startConnection() {
    try {
      await this.hubConnection.start();
    } catch (err) {
      console.error('SignalR Connection Error:', err);

      setTimeout(() => this.startConnection(), 5000);
    }
  }

  public getMessageStream() {
    return this.messageReceived$.asObservable();
  }

  public sendMessage(message: any) {
    this.hubConnection
      .invoke('SendMessage', message)
      .then(() => {})
      .catch((err) => {
        console.error('Error sending message:', err);
      });
  }

  public deleteMessage(messageId: any) {
    this.hubConnection
      .invoke('DeleteMessage', messageId)
      .catch((err) => console.error('Error deleting message:', err));
  }
  public getDeletedMessageStream() {
    return this.messageDeleted$.asObservable();
  }
  public markMessagesAsRead(senderId: string): Promise<void> {
    return this.hubConnection
      .invoke('MarkMessagesAsRead', senderId)
      .catch((err) => console.error('Error marking messages as read:', err));
  }
  public getReadMessageStream() {
    return this.messagesRead$.asObservable();
  }
}
