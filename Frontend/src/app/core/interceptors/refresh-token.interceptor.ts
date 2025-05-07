import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError, of } from 'rxjs';
import { AuthService } from '../../features/auth/services/auth.service';
import { Router } from '@angular/router';

export const refreshTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const token = authService.getToken();

  if (!token || !authService.isLoggedIn()) {
    return next(req);
  }

  if (!authService.isTokenExpired()) {
    const clonedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
    return next(clonedReq);
  }

  // Token expired: try refreshing
  return authService.refreshToken().pipe(
    switchMap((newToken: string) => {
      if (!newToken) {
        router.navigate(['/auth/login']);
        return throwError(() => new Error('Unable to refresh token'));
      }

      const newReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${newToken}`,
        },
      });

      return next(newReq);
    }),
    catchError((error: HttpErrorResponse) => {
      authService.logout();
      router.navigate(['/auth/login']);
      return throwError(() => error);
    })
  );
};
