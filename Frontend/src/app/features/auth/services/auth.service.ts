import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Login } from '../../../core/interfaces/login';
import { Observable, of, switchMap, tap } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';
import { ChangePassword } from '../../../core/interfaces/changePassword';
import { Register } from '../../../core/interfaces/register';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenKey = 'auth_token';
  private refreshKey = 'refresh_token';

  constructor(
    private http: HttpClient,
    private cookies: CookieService,
    private router: Router
  ) {}

  login(model: Login): Observable<any> {
    return this.http
      .post<any>(`${environment.apiUrl}/Authentication/login`, model)
      .pipe(
        tap((response) => {
          const accessToken = response.data.accessToken;
          const refreshToken = response.data.refreshToken;

          if (accessToken) {
            this.cookies.set(this.tokenKey, accessToken, {
              path: '/',
              sameSite: 'Lax',
              secure: true,
            });
          }
          if (refreshToken) {
            this.cookies.set(this.refreshKey, refreshToken, {
              path: '/',
              sameSite: 'Lax',
              secure: true,
            });
          }
        })
      );
  }

  logout(): void {
    this.cookies.delete(this.tokenKey, '/');
    this.cookies.delete(this.refreshKey, '/');
    this.router.navigate(['/auth/login']);
  }

  getToken(): string | null {
    return this.cookies.get(this.tokenKey);
  }

  isLoggedIn(): boolean {
    const accessToken = this.cookies.get(this.tokenKey);
    const refreshToken = this.cookies.get('refresh_token');

    return !!accessToken || !!refreshToken;
  }

  isTokenExpired(): boolean {
    const token = this.cookies.get(this.tokenKey);
    if (!token) return true;

    try {
      const decoded: any = jwtDecode(token);
      const exp = decoded.exp;
      const now = Math.floor(Date.now() / 1000);

      // هامش أمان 60 ثانية
      return exp < now + 60;
    } catch {
      return true;
    }
  }

  refreshToken(): Observable<string> {
    const accessToken = this.getToken();
    const refreshToken = this.cookies.get(this.refreshKey);

    if (!accessToken || !refreshToken) {
      this.logout();
      return of('');
    }

    const body = {
      accessToken,
      refreshToken,
    };

    return this.http
      .post<any>(`${environment.apiUrl}/Authentication/refresh`, body)
      .pipe(
        tap((response) => {
          const newAccessToken = response.accessToken;
          const newRefreshToken = response.refreshToken;

          if (newAccessToken) {
            this.cookies.set(this.tokenKey, newAccessToken);
          }
          if (newRefreshToken) {
            this.cookies.set(this.refreshKey, newRefreshToken);
          }
        }),
        switchMap((response) => of(response.accessToken))
      );
  }

  getRole(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded.role || null;
    } catch {
      return null;
    }
  }

  getUserEmail(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded.email || null;
    } catch {
      return null;
    }
  }

  getUserName(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded.name || null;
    } catch {
      return null;
    }
  }
  getUserGovernorate(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded.governorate || null;
    } catch {
      return null;
    }
  }

  getUserId(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded.id || null;
    } catch {
      return null;
    }
  }

  forgetPassword(model: { email: string }) {
    return this.http.post(
      `${environment.apiUrl}/Authentication/forget-password`,
      model
    );
  }

  changePass(model: ChangePassword) {
    return this.http.post<any>(
      `${environment.apiUrl}/Authentication/reset-password`,
      model
    );
  }

  signUp(model: Register): Observable<any> {
    return this.http
      .post<any>(`${environment.apiUrl}/Authentication/register`, model)
      .pipe(
        tap((response) => {
          const accessToken = response.data.accessToken;
          const refreshToken = response.data.refreshToken;

          if (accessToken) {
            this.cookies.set(this.tokenKey, accessToken, {
              path: '/',
              sameSite: 'Lax',
              secure: true,
            });
          }
          if (refreshToken) {
            this.cookies.set(this.refreshKey, refreshToken, {
              path: '/',
              sameSite: 'Lax',
              secure: true,
            });
          }
        })
      );
  }
  getHealthUnits(governorate: any, cityCenter: any) {
    return this.http.get<any>(
      `${environment.apiUrl}/HealthCare/healthCares?Governorate=${governorate}&City=${cityCenter}`
    );
  }
}
