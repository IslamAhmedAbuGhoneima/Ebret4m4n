import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

export const roleGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const cookieService = inject(CookieService); // استخدمنا الكوكيز هنا
  const expectedRole = route.data['role'];
  const userRole = cookieService.get('role'); // جلب الدور من الكوكي

  if (expectedRole === userRole) {
    return true;
  }

  // router.navigate(['/no-permission']); // توجيه في حالة عدم وجود صلاحية
  return false;
};
