export interface Message {
  message?: string | null;
  senderId: string;
  receiverId: string;
  sendAt: string;
  File?: string | null;
}
