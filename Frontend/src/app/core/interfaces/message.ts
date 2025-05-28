export interface Message {
  message?: string | null;
  senderId: string;
  recieverId: string;
  sendAt: string;
  File?: string | null;
}
