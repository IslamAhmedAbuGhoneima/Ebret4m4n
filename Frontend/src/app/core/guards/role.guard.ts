import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../../features/auth/services/auth.service';

export const roleGuard: CanActivateFn = async (route, state) => {
  const router = inject(Router);
  const authService = inject(AuthService);

  const token = authService.getToken();
  if (!token || authService.isTokenExpired()) {
    router.navigate(['/auth/login']);
    return false;
  }

  try {
    const decoded: any = jwtDecode(token);
    const userRole = decoded?.role || null;
    const expectedRole = route.data['role'];
    const expectedRoles = route.data['roles'];

    if (expectedRoles?.includes(userRole)) return true;
    if (expectedRole && userRole === expectedRole) return true;
  } catch {
    router.navigate(['/auth/login']);
    return false;
  }

  router.navigate(['/not-found']);
  return false;
};
