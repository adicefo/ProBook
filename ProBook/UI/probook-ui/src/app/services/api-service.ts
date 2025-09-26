import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
    providedIn: 'root'
})
export class ApiService {
    private baseUrl = "https://localhost:7012";

    constructor(private http: HttpClient) { }

    private getHeaders(): HttpHeaders {
        const token = localStorage.getItem('auth_token');
        let headers = new HttpHeaders().set('Content-Type', 'application/json');

        if (token) {
            headers = headers.set('Authorization', `Basic ${token}`);
        }

        return headers;
    }

    get<T>(endpoint: string): Observable<T> {
        return this.http.get<T>(`${this.baseUrl}/${endpoint}`, {
            headers: this.getHeaders(),
            withCredentials: true
        });
    }

    getById<T>(endpoint: string, id: number | string): Observable<T> {
        return this.http.get<T>(`${this.baseUrl}/${endpoint}/${id}`, {
            headers: this.getHeaders(),
            withCredentials: true
        });
    }

    post<T>(endpoint: string, body: any): Observable<T> {
        return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body, {
            headers: this.getHeaders(),
            withCredentials: true
        });
    }

    put<T>(endpoint: string, id: number | string, body: any): Observable<T> {
        return this.http.put<T>(`${this.baseUrl}/${endpoint}/${id}`, body, {
            headers: this.getHeaders(),
            withCredentials: true
        });
    }

    delete<T>(endpoint: string, id: number | string): Observable<T> {
        return this.http.delete<T>(`${this.baseUrl}/${endpoint}/${id}`, {
            headers: this.getHeaders(),
            withCredentials: true
        });
    }

}