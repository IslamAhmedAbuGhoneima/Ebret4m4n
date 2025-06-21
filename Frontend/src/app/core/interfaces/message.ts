export interface Message {
  id?: string;
  message?: string | null;
  senderId: string;
  receiverId: string;
  sentAt: string;
  file?: string | null;
  isRead?: boolean;
}
