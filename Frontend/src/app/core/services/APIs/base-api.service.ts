import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BaseApiService {
  constructor(protected http: HttpClient) {}

  get<T>(url: string, options?: any) {
    return this.http.get<T>(url, options);
  }

  post<T>(url: string, data: any, options?: any) {
    return this.http.post<T>(url, data, options);
  }

  put<T>(url: string, data: any, options?: any) {
    return this.http.put<T>(url, data, options);
  }

  delete<T>(url: string, options?: any) {
    return this.http.delete<T>(url, options);
  }
}
