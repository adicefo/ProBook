import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = 'https://localhost:7012';

  constructor(private http: HttpClient) {}

  private getHeaders(isFormData:boolean=false): HttpHeaders {
    const token = localStorage.getItem('auth_token');
    let headers = new HttpHeaders();
    if (!isFormData) {
      headers = headers.set('Content-Type', 'application/json');
    }
  

    if (token) {
      headers = headers.set('Authorization', `Basic ${token}`);
    }

    return headers;
  }

  get<T>(endpoint: string, params: any = {}): Observable<T> {
    let url = `${this.baseUrl}/${endpoint}`;
    if (params && Object.keys(params).length > 0) {
      const queryString = this.getQueryString(params);
      url = `${url}?${queryString}`;
    }
    return this.http.get<T>(url, {
      headers: this.getHeaders(),
      withCredentials: true,
    });
  }

  getById<T>(endpoint: string, id: number | string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}/${id}`, {
      headers: this.getHeaders(),
      withCredentials: true,
    });
  }

  post<T>(endpoint: string, body: any): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body, {
      headers: this.getHeaders(),
      withCredentials: true,
    });
  }
  postFormData<T>(endpoint: string, body: any): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body, {
      headers: this.getHeaders(true),
      withCredentials: true,
    });
  }
  put<T>(endpoint: string, id: number | string, body: any): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${endpoint}/${id}`, body, {
      headers: this.getHeaders(),
      withCredentials: true,
    });
  }

  delete<T>(endpoint: string, id: number | string): Observable<T> {
    return this.http.delete<T>(`${this.baseUrl}/${endpoint}/${id}`, {
      headers: this.getHeaders(),
      withCredentials: true,
    });
  }
  private getQueryString(
    params: any,
    prefix: string = '',
    inRecursion: boolean = false
  ): string {
    let query = '';

    Object.keys(params).forEach((key) => {
      const value = params[key];
      if (value === null || value === undefined) {
        return; // skip null/undefined
      }

      if (
        typeof value === 'object' &&
        !Array.isArray(value) &&
        !(value instanceof Date)
      ) {
        // nested object
        query += this.getQueryString(value, `${prefix}${key}.`, true);
      } else if (Array.isArray(value)) {
        // array handling
        value.forEach((item) => {
          if (
            item !== null &&
            item !== undefined &&
            item.toString().trim() !== ''
          ) {
            query += `${encodeURIComponent(
              prefix ? `${prefix}${key}` : key
            )}=${encodeURIComponent(item.toString().trim())}&`;
          }
        });
      } else {
        // date or simple value
        let val = value;
        if (value instanceof Date) {
          val = `${value.getFullYear()}-${(value.getMonth() + 1)
            .toString()
            .padStart(2, '0')}-${value.getDate().toString().padStart(2, '0')}`;
        }
        query += `${encodeURIComponent(
          prefix ? `${prefix}${key}` : key
        )}=${encodeURIComponent(val.toString().trim())}&`;
      }
    });

    // remove trailing "&" if not in recursion
    return inRecursion ? query : query.replace(/&$/, '');
  }
}
