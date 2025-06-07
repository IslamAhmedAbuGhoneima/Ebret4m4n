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
  unreadCount: any;
  @ViewChild('scrollContainer') private scrollContainer!: ElementRef;
  selectedMessage: Message | null = null;

  constructor(
    private authService: AuthService,
    private _ChatService: ChatService,
    private _NotificationService: NotificationService,
    private _ActivatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.role = this.authService.getRole();
    this.senderId = this.authService.getUserId();

    // Set up message stream subscription
    this._ChatService.getMessageStream().subscribe((msg: Message) => {
      console.log('Message received in component:', msg);
      
      // Check if the message is relevant to the current chat
      const isRelevantMessage = 
        (this.role === 'parent' && msg.senderId === this.selectedDoctorId) ||
        (this.role === 'doctor' && msg.senderId === this.selectedUserId) ||
        (msg.senderId === this.senderId && 
          ((this.role === 'parent' && msg.receiverId === this.selectedDoctorId) ||
           (this.role === 'doctor' && msg.receiverId === this.selectedUserId)));

      if (isRelevantMessage) {
        this.messages.push(msg);
        this.scrollToBottom();
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

    if (this.role == 'parent') {
      this._ChatService.getHealthcareDoctorId().subscribe({
        next: (res: any) => {
          this.selectedDoctorId = res.data.doctorId;
          this.user = res.data.doctorName;

          this._ChatService
            .getMessages(this.selectedDoctorId)
            .subscribe((msgs: any) => {
              this.messages = msgs.data;
              this.scrollToBottom();
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
      });
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  private scrollToBottom(): void {
    try {
      if (this.scrollContainer) {
        this.scrollContainer.nativeElement.scrollTop = 
          this.scrollContainer.nativeElement.scrollHeight;
      }
    } catch (err) {
      console.error('Error scrolling to bottom:', err);
    }
  }

  sendMessage() {
    if (!this.newMessage.trim()) return;

    const message = {
      message: this.newMessage,
      senderId: this.senderId,
      receiverId: this.role === 'parent' ? this.selectedDoctorId : this.selectedUserId,
      sentAt: new Date()
    };

    this._ChatService.sendMessage(message);
    this.newMessage = '';
  }

  deleteMessage(message: Message) {
    if (!message.id) {
      console.error('Cannot delete message: No message ID');
      return;
    }
    console.log('Deleting message with ID:', message.id);
    this._ChatService.deleteMessage(message.id);
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
      this.messages.push(msg);
      this._ChatService.sendMessage(msg);
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

  isImage(filePath: string): boolean {
    const imageExtensions = ['.png', '.jpg', '.jpeg', '.gif', '.bmp', '.webp'];
    const ext = filePath.split('.').pop()?.toLowerCase();
    return !!ext && imageExtensions.includes('.' + ext);
  }
}
