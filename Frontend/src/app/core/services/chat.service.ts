import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Message } from '../interfaces/message';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  constructor(private http: HttpClient) {}
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
}
