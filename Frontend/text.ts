// this._ChatService.getMessageStream().subscribe((msg: any) => {
//   if (msg.senderId === this.senderId) {
//     // رسالة أرسلناها نحن؛ إما رد السيرفر أو رسالة مؤقتة
//     if (msg.isTemp) {
//       // إضافة مؤقتة UI، لكن في حالنا نضيف بالفعل مسبقاً في sendMessage.
//       // إن أضفنا في sendMessage، هنا نتجاهل msg.isTemp.
//     } else {
//       // هذه رد السيرفر: يحتوي على الحقل tempId نفسه الذي أرسلناه
//       const idx = this.messages.findIndex(
//         (m) => m.tempId && m.tempId === msg.tempId
//       );
//       if (idx !== -1) {
//         // نستبدل الرسالة المؤقتة بالرسالة الحقيقية
//         this.messages[idx] = msg;
//       } else {
//         // إن لم توجد رسالة مؤقتة، نضيفها
//         this.messages.push(msg);
//       }
//     }
//   } else if (msg.receiverId === this.senderId) {
//     // رسالة واردة من الآخر
//     // ... المنطق كما في كودك للحالة parent أو doctor
//     this.messages.push(msg);
//     // ثم نرسل علامة مقروئية:
//     this._ChatService.markMessagesAsRead(msg.senderId);
//   }
// });
