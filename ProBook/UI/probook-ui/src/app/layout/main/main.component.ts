import { Component } from '@angular/core';
import { Router, RouterModule, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    RouterLink,
    RouterLinkActive,
    MaterialModule
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {
  username: string = 'User';

  constructor(private router: Router) { }

  logout(): void {
    localStorage.removeItem('auth_token');
    this.router.navigate(['/']);
  }
}
