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
  messages: any = [];
  user = 'User' + Math.floor(Math.random() * 1000);
  newMessage = '';
  selectedDoctorId: any;
  senderId: any;

  constructor(
    private authService: AuthService,
    private _ChatService: ChatService,
    private _SignalRService: SignalRService
  ) {}

  ngOnInit(): void {
    this.role = this.authService.getRole();
    this.senderId = this.authService.getUserId();
    if (this.role == 'parent') {
      this._ChatService.getHealthcareDoctorId().subscribe({
        next: (res: any) => {
          this.selectedDoctorId = res.data.doctorId;
          this.user = res.data.user;
          this._ChatService
            .getMessages(this.selectedDoctorId)
            .subscribe((msgs: Message[]) => {
              this.messages = msgs;
            });
          this._SignalRService.getMessageStream().subscribe((msg: Message) => {
            if (
              msg.senderId === this.selectedDoctorId ||
              msg.receiverId === this.selectedDoctorId
            ) {
              this.messages.push(msg);
            }
          });

          this._SignalRService.getMessageStream().subscribe((msg: Message) => {
            if (
              msg.senderId === this.selectedDoctorId ||
              msg.receiverId === this.selectedDoctorId
            ) {
              this.messages.push(msg);
            }
          });
        },
      });
    }
  }

  sendMessage(): void {
    if (this.newMessage.trim()) {
      const msg: Message = {
        message: this.newMessage,
        File: null,
        senderId: this.senderId,
        receiverId: this.selectedDoctorId,
        sendAt: new Date().toISOString(),
      };

      this._SignalRService.sendMessage(msg);
      this.newMessage = '';
    }
  }
  sendFile(event: any): void {
    // const file = event.target.files[0];
    // const formData = new FormData();
    // formData.append('file', file);
    // this._ChatService.uploadFile(formData).subscribe((res: any) => {
    //   const msg: Message = {
    //     message: null,
    //     fileUrl: res.fileUrl, // هذا يتم إرجاعه من API رفع الملف
    //     senderId: this.senderId,
    //     recieverId: this.selectedDoctorId,
    //     sendAt: new Date().toISOString(),
    //   };
    //   this._SignalRService.sendMessage(msg);
    // });
  }
}
