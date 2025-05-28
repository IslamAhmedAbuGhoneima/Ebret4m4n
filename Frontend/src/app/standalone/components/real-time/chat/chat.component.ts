import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { SignalRService } from '../../../../core/services/signalR.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Message } from '../../../../core/interfaces/message';
import { ChatService } from '../../../../core/services/chat.service';

@Component({
  selector: 'app-chat',
  imports: [CommonModule, FormsModule],
  providers: [SignalRService],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  role: any;
  messages: any;
  user = 'User' + Math.floor(Math.random() * 1000); // اسم مؤقت
  newMessage = '';
  selectedDoctorId: any;

  constructor(
    private authService: AuthService,
    private _ChatService: ChatService,
    private _SignalRService: SignalRService
  ) {}

  ngOnInit(): void {
    this.authService.getRole();

    // Step 1: جيبي الـ doctorId
    this._ChatService.getHealthcareDoctorId().subscribe((doctorId: string) => {
      this.selectedDoctorId = doctorId;

      // Step 2: جيبي المحادثة معاه
      this._ChatService.getMessages(doctorId).subscribe((msgs: Message[]) => {
        this.messages = msgs;
      });

      // Step 3: استقبلي الرسائل الجديدة من SignalR
      // this._SignalRService.getMessageStream().subscribe((msg: Message) => {
      //   if (msg.user === doctorId || msg.receiver === doctorId) {
      //     this.messages.push(msg);
      //   }
      // });
    });
  }

  sendMessage(): void {
    if (this.newMessage.trim()) {
      const msg: Message = {
        user: this.user,
        content: this.newMessage,
        timestamp: new Date().toISOString(),
      };

      // ✅ إرسال الرسالة للباك إند (REST)
      // this._ChatService.sendMessage(msg).subscribe(() => {
      //   this.newMessage = ''; // تنظيف الحقل بعد الإرسال
      // });

      // ✅ بث الرسالة عبر SignalR
      this._SignalRService.sendMessage(msg);
    }
  }
}
