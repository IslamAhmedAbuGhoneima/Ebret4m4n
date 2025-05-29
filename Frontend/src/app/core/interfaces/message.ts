export interface Message {
  message?: string | null;
  senderId: string;
  receiverId: string;
  sentAt: string;
  File?: string | null;
}
