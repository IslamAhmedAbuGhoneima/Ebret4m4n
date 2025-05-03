import { HttpInterceptorFn } from '@angular/common/http';

export const catchErrorInterceptor: HttpInterceptorFn = (req, next) => {
  // const toastr = inject(ToastrService);
  // const router = inject(Router);
  // const cookies = inject(CookieService);

  // return next(req).pipe(
  //   catchError((error: HttpErrorResponse) => {
  //     const msg = error.error?.message || error.message || 'حدث خطأ غير متوقع';

  //     // إذا كانت المشكلة متعلقة بصلاحية التوكن (مثلاً JWT expired أو malformed)
  //     if (msg.toLowerCase().includes('jwt')) {
  //       // حذف الكوكيز
  //       cookies.delete('auth_token', '/');
  //       // إعادة التوجيه إلى صفحة تسجيل الدخول
  //       router.navigate(['/auth/login']);
  //     } else {
  //       // عرض رسالة الخطأ للمستخدم
  //       toastr.error(msg);
  //     }

  //     // إعادة رمي الخطأ للاستدعاء الأصلي إذا احتجت معالجته هناك أيضًا
  //     return throwError(() => error);
  //   })
  // );

  return next(req);
};
