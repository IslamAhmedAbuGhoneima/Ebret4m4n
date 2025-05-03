import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const cookieService = inject(CookieService);
  const token = cookieService.get('auth_token');
  const newRequest = req.clone({
    headers: req.headers.append('Authorization', `Bearer ${token}`),
  });
  return next(newRequest);
};
