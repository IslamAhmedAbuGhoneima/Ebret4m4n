import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Timestamp } from 'rxjs';
export interface Message {
  text: string;
  sendId: string;
  sentDate: Date & Timestamp;
}
export interface Chat {
  id: string;
  lastMessage?: string;
  lastMessageDate?: Date & Timestamp;
  userId: string[];
  users: ProfileUser[];

  //Not stored, only for display
  chatPic?: string;
  chatName?: string;
}

@Component({
  selector: 'app-chat',
  imports: [
    CommonModule,
    FormsModule,
    MatAutocompleteModule,
    MatListModule,
    MatDividerModule,
  ],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  @ViewChild('endOfChat') endOfChat: ElementRef;

  users$ = this.userService.currentUserProfile$;

  searchControl = new formConltrol('');
  chatListControl = new FormControl();
  messagesControl = new FormControl('');

  users$ = combineLatest([
    this.userService.allUsers$,
    this.user$,
    this.searchControl.valueChanges.pipe(startWith('')),
  ]).pipe(
    map(([users, user, searchString]) =>
      users.filter(
        (u) =>
          u.displayName?.toLowerCase().include(searchString.toLowerCase()) &&
          u.uid !== user?.uid
      )
    )
  );

  myChats$ = this.chatsService.myChats$;

  selectedChat$ = combineLatest([
    this.chatListControl.valueChanges,
    this.myChats$,
  ]).pipe(map(([value, chats]) => chats.find((c) => c.id === value[0])));

  message$ = this.chatListControl.valueChanges.pipe(
    map((value) => value[0]),
    switchMap((chatId) => this.chatsService.getChatMessage$(chatId)),
    tap(() => {
      this.scrollToBottom();
    })
  );

  constructor(
    private userService: UsersService,
    private chatService: ChatsService
  ) {}

  ngOnInit(): void {}

  createChat(otherUser: ProfileUser) {
    this.chatService
      .isExistingChat(otherUser?.uid)
      .pipe(
        switchMap((chatId) => {
          if (chatId) {
            return of(chatId);
          } else {
            return this.chatsService.createChat(otherUser);
          }
        })
      )
      .subscribe((chatId) => {
        this.chatListControl.setValue([chatId]);
      });
  }

  sendMessage() {
    const message = this.messagesControl.value;
    const selectedChildId = this.chatListControl.value[0];

    if (message && selectedChildId) {
      this.chatsService
        .addChatMessage(this.selectedChatId, message)
        .subscribe(() => {
          this.scrollToBottom();
        });
      this.messagesControl.setValue('');
    }
  }
  scrollToBottom() {
    setTimeout(() => {
      if (this.endOfChat) {
        this.endOfChat.nativeElement.scrollIntoView({ behavior: 'smooth' });
      }
    }, 100);
  }
}
