export interface Message {
  id?: any;
  message?: string | null;
  senderId: string;
  receiverId: string;
  sentAt: string;
  file?: string | null;
  isRead?: boolean;
}
