import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { Subject } from 'rxjs';
@Injectable({
  providedIn: 'root', // لازم تكون دي موجودة
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  private messageReceived$ = new Subject<any>();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/chathub') // Endpoint بتاع الهب
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on('ReceiveMessage', (data) => {
      this.messageReceived$.next(data); // إرسال البيانات للمشتركين
    });

    this.hubConnection.start().catch((err) => console.error(err));
  }

  public getMessageStream() {
    return this.messageReceived$.asObservable();
  }

  public sendMessage(message: any) {
    this.hubConnection
      .invoke('SendMessage', message)
      .catch((err) => console.error(err));
  }
}
