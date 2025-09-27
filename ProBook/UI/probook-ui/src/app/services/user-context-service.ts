// user-context.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { User } from '../interfaces/user-interface';

@Injectable({ providedIn: 'root' })
export class UserContextService {
  private userSubject = new BehaviorSubject<User | null>(null);
  user$ = this.userSubject.asObservable();

  setUser(user: User) {
    this.userSubject.next(user);
  }

  getCurrentUser(): User | null {
    return this.userSubject.value;
  }
}
