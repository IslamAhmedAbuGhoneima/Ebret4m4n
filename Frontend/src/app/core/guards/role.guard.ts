import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';

export const roleGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const cookieService = inject(CookieService);

  const token = cookieService.get('auth_token');
  if (!token) {
    router.navigate(['/auth/login']);
    return false;
  }

  let userRole: string | null = null;
  try {
    const decoded: any = jwtDecode(token);
    userRole = decoded?.role || null;
  } catch (error) {
    router.navigate(['/auth/login']);
    return false;
  }

  const expectedRole = route.data['role'];
  const expectedRoles = route.data['roles'];

  if (expectedRoles && Array.isArray(expectedRoles)) {
    if (expectedRoles.includes(userRole)) {
      return true;
    }
  }

  if (expectedRole && userRole === expectedRole) {
    return true;
  }

  router.navigate(['/not-found']);
  return false;
};
