import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { SignalRService } from '../../../../core/services/signalR.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Message } from '../../../../core/interfaces/message';
import { ChatService } from '../../../../core/services/chat.service';
import { ActivatedRoute, RouterModule } from '@angular/router';

@Component({
  selector: 'app-chat',
  imports: [CommonModule, FormsModule, RouterModule],
  providers: [SignalRService],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  role: any;
  messages: any = [];
  user: any;
  newMessage = '';
  selectedDoctorId: any;
  senderId: any;
  @ViewChild('messageInput') messageInput!: ElementRef;
  userChatList: any;
  selectedUserId: any;
  selectedUser: any;
  showWelcomeImage = true; // ✅ [NEW] تحكم في ظهور صورة البداية للطبيب

  constructor(
    private authService: AuthService,
    private _ChatService: ChatService,
    private _SignalRService: SignalRService,
    private _ActivatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.role = this.authService.getRole();
    this.senderId = this.authService.getUserId();
    if (this.role == 'parent') {
      this._ChatService.getHealthcareDoctorId().subscribe({
        next: (res: any) => {
          this.selectedDoctorId = res.data.doctorId;
          this.user = res.data.doctorName;

          this._ChatService
            .getMessages(this.selectedDoctorId)
            .subscribe((msgs: any) => {
              console.log('messages from API:', msgs.data);

              this.messages = msgs.data;
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
    } else if (this.role === 'doctor') {
      this._ChatService.getUserChatList().subscribe({
        next: (res: any) => {
          this.userChatList = res.data;
          this._ActivatedRoute.paramMap.subscribe((params) => {
            const userId = params.get('userId');
            if (userId) {
              this.selectUser(userId);
            }
          });
        },
      });
    }
  }
  selectUser(id: string) {
    this.selectedUserId = id;
    this.selectedUser = this.userChatList.find(
      (user: any) => user.senderId === id
    );
    this.user = `${this.selectedUser.firstName} ${this.selectedUser.lastName}`;
    this.showWelcomeImage = false;

    this._ChatService
      .getMessages(this.selectedUserId)
      .subscribe((msgs: any) => {
        this.messages = msgs.data;
      });

    this._SignalRService.getMessageStream().subscribe((msg: Message) => {
      if (
        msg.senderId === this.selectedUserId ||
        msg.receiverId === this.selectedUserId
      ) {
        this.messages.push(msg);
      }
    });
  }

  sendMessage(): void {
    if (this.newMessage.trim()) {
      const receiverId =
        this.role === 'parent' ? this.selectedDoctorId : this.selectedUserId;

      const msg: Message = {
        message: this.newMessage,
        File: null,
        senderId: this.senderId,
        receiverId: receiverId,
        sentAt: new Date().toISOString(),
      };

      this.messages.push(msg);
      this._SignalRService.sendMessage(msg);
      this.newMessage = '';

      if (this.messageInput) {
        const textarea = this.messageInput.nativeElement as HTMLTextAreaElement;
        textarea.style.height = 'auto';
        textarea.style.height = '24px';
      }
    }
  }
  sendFile(event: any): void {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.onload = () => {
      const base64File = reader.result as string;

      const receiverId =
        this.role === 'parent' ? this.selectedDoctorId : this.selectedUserId;

      const msg: Message = {
        message: null,
        File: base64File,
        senderId: this.senderId,
        receiverId: receiverId,
        sentAt: new Date().toISOString(),
      };

      this.messages.push(msg);
      this._SignalRService.sendMessage(msg);
    };
    reader.readAsDataURL(file);
  }
  formatTime(dateString: string | null | undefined): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    let hours = date.getHours();
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12 || 12;
    return `${hours}:${minutes} ${ampm}`;
  }

  onEnter(event: any) {
    if (event.shiftKey) {
      return;
    }
    event.preventDefault();
    this.sendMessage();
  }

  isLastMessageOfSender(index: number): boolean {
    const currentMsg = this.messages[index];
    const nextMsg = this.messages[index + 1];
    return !nextMsg || currentMsg.senderId !== nextMsg.senderId;
  }

  autoResize(event: Event) {
    const textarea = event.target as HTMLTextAreaElement;
    textarea.style.height = 'auto';
    textarea.style.height = textarea.scrollHeight + 'px';
  }
}
