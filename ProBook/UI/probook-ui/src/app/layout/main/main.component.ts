import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  imports: [],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {
  username: string = 'User'; // Replace with real user data later

  constructor(private router: Router) {}

  logout(): void {
   
    localStorage.removeItem('auth_token');
    this.router.navigate(['/login']);
  }
}
