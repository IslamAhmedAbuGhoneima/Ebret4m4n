import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Login } from '../../../core/models/login';
import { Observable, Subject, tap } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  user = new Subject();
  private tokenKey = 'auth_token';

  constructor(private http: HttpClient, private cookies: CookieService) {}

  login(model: Login): Observable<any> {
    return this.http
      .post<any>(`${environment.apiUrl}/Authentication/login`, model)
      .pipe(
        tap((response) => {
          const token = response.data.accessToken;
          if (token) {
            // تخزين التوكن فقط داخل الكوكي
            this.cookies.set(this.tokenKey, token);
          }
        })
      );
  }

  getRole(): string | null {
    const token = this.cookies.get(this.tokenKey);
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded.role || null;
    } catch (e) {
      return null;
    }
  }

  getUserEmail(): string | null {
    const token = this.cookies.get(this.tokenKey);
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded.email || null;
    } catch (e) {
      return null;
    }
  }

  getUserName(): string | null {
    const token = this.cookies.get(this.tokenKey);
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded.name || null;
    } catch (e) {
      return null;
    }
  }

  logout(): void {
    this.cookies.delete(this.tokenKey, '/');
  }

  isLoggedIn(): boolean {
    return !!this.cookies.get(this.tokenKey);
  }
}
