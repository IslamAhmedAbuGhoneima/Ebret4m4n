export interface Order {
  id: number;
  status: 'قيد الإنتظار' | 'مستلمة' | 'غير مستلمة' | 'جارى التوصيل';
  center?: string;
  order?: string;
}
