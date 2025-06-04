import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
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
  constructor(private authService: AuthService, private http: HttpClient) {
    // Get current user ID from token
    this.currentUserId = this.authService.getUserId();

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5112/chat', {
        accessTokenFactory: () => {
          const token = this.authService.getToken();
          console.log('SignalR Token:', token);
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
        console.log('On SignalR target - Received message as receiver:', data);
        this.messageReceived$.next(data);
      } else if (data.senderId === this.currentUserId) {
        // If we're the sender, just log it but don't emit
        console.log('On SignalR target - Sent message as sender:', data);
      }
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
  } //doctor
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
}
