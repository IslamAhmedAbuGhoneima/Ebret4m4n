import {
  AfterViewChecked,
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Message } from '../../../../core/interfaces/message';
import { ChatService } from '../../../../core/services/chat.service';
import {
  ActivatedRoute,
  NavigationEnd,
  Router,
  RouterModule,
} from '@angular/router';
import { NotificationService } from '../../../../core/services/notification.service';

@Component({
  selector: 'app-chat',
  imports: [CommonModule, FormsModule, RouterModule],
  providers: [],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit, AfterViewChecked {
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
  showWelcomeImage = true;
  chatImages: any;

  @ViewChild('scrollContainer') private scrollContainer!: ElementRef;
  selectedMessage: Message | null = null;

  constructor(
    private authService: AuthService,
    private _ChatService: ChatService,
    private _NotificationService: NotificationService,
    private _ActivatedRoute: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.role = this.authService.getRole();
    this.senderId = this.authService.getUserId();

    // Set up message stream subscription for both roles
    this._ChatService.getMessageStream().subscribe((msg: Message) => {
      // Only add the message if we're the receiver
      if (msg.receiverId === this.senderId) {
        if (this.role === 'parent') {
          if (msg.senderId === this.selectedDoctorId) {
            this.messages.push(msg);
          }
        } else if (this.role === 'doctor' && this.selectedUserId) {
          if (msg.senderId === this.selectedUserId) {
            this.messages.push(msg);
          }
        }
      }
    });

    this._ChatService
      .getDeletedMessageStream()
      .subscribe((messageId: string) => {
        const index = this.messages.findIndex(
          (msg: Message) => msg.id === messageId
        );
        if (index !== -1) {
          this.messages.splice(index, 1);
        }
      });

    this._ChatService.getReadMessageStream().subscribe((data: any) => {
      const { ReceiverId, MessageIds } = data;

      this.messages.forEach((msg: any) => {
        if (MessageIds.includes(msg.id)) {
          msg.isRead = true;
        }
      });
      this.messages = [...this.messages];

      this.cdr.detectChanges();
    });

    if (this.role == 'parent') {
      this._ChatService.getHealthcareDoctorId().subscribe({
        next: (res: any) => {
          this.selectedDoctorId = res.data.doctorId;
          this.user = res.data.doctorName;

          this._ChatService
            .getMessages(this.selectedDoctorId)
            .subscribe((msgs: any) => {
              this.messages = msgs.data;

              this._ChatService.markMessagesAsRead(this.selectedDoctorId);
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
        this.scrollToBottom();

        this._ChatService
          .markMessagesAsRead(this.selectedUserId)
          .then(() => {
            this._ChatService.getUserChatList().subscribe((res: any) => {
              this.userChatList = res.data;
            });
          })
          .catch((err: any) =>
            console.error('Error marking messages as read:', err)
          );
      });
  }

  sendMessage() {
    if (!this.newMessage.trim()) return;

    const message = {
      message: this.newMessage,
      senderId: this.senderId,
      receiverId:
        this.role === 'parent' ? this.selectedDoctorId : this.selectedUserId,
      sentAt: new Date(),
    };

    this._ChatService.sendMessage(message);
    this.messages.push(message);

    this.newMessage = '';
  }

  isLastMessageOfSender(index: number): boolean {
    const currentMsg = this.messages[index];
    const nextMsg = this.messages[index + 1];
    return !nextMsg || currentMsg.senderId !== nextMsg.senderId;
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
        file: base64File,
        senderId: this.senderId,
        receiverId: receiverId,
        sentAt: new Date().toISOString(),
      };
      this._ChatService.sendMessage(msg);

      this.messages.push(msg);
    };

    reader.readAsDataURL(file);
  }

  formatTime(dateString: string | null | undefined): string {
    if (!dateString) return '';

    const date = new Date(dateString);
    let hours = date.getHours();
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const ampm = hours >= 12 ? 'م' : 'ص';
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

  autoResize(event: Event) {
    const textarea = event.target as HTMLTextAreaElement;
    textarea.style.height = 'auto';
    textarea.style.height = textarea.scrollHeight + 'px';
  }

  isImage(filePath: any): boolean {
    if (!filePath) return false;

    // Check if it's a base64 image
    if (filePath.startsWith('data:image')) {
      return true;
    }

    // Check if it's a file path with an image extension
    const imageExtensions = ['.png', '.jpg', '.jpeg', '.gif', '.bmp', '.webp'];
    const match = filePath.match(/\.\w+(?=($|\?))/); // match extension
    const ext = match ? match[0].toLowerCase() : '';

    return imageExtensions.includes(ext);
  }
  getFileUrl(file: string): string {
    if (!file) return '';

    if (file.startsWith('data:image')) {
      return file;
    } else {
      return 'http://localhost:5112' + file;
    }
  }
  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  private scrollToBottom(): void {
    if (this.scrollContainer?.nativeElement) {
      this.scrollContainer.nativeElement.scrollTop =
        this.scrollContainer.nativeElement.scrollHeight;
    }
  }

  canDelete(msg: any): boolean {
    if (!msg || !msg.sentAt) {
      return false;
    }
    const now = Date.now();
    const sentTime = new Date(msg.sentAt).getTime();
    const thMinutes = 10 * 60 * 1000;
    return msg.senderId === this.senderId && now - sentTime < thMinutes;
  }
  deleteMessage(msg: any): void {
    const index = this.messages.indexOf(msg);
    if (index !== -1) {
      this.messages.splice(index, 1);
      this._ChatService.deleteMessage(msg.id);
    }
  }
}
