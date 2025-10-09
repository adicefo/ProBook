import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  login(username: string, password: string): void {
    const token = btoa(`${username}:${password}`);
    localStorage.setItem('auth_token', token);

  }

  logout(): void {
    localStorage.removeItem('auth_token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('auth_token');
  }

  updateToken(username: string, newPassword: string): void {
    const token = btoa(`${username}:${newPassword}`);
    localStorage.setItem('auth_token', token);
  }

  getCurrentUsername(): string | null {
    const token = localStorage.getItem('auth_token');
    if (!token) {
      return null;
    }
    try {
      const decoded = atob(token);
      const username = decoded.split(':')[0];
      return username;
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }
}