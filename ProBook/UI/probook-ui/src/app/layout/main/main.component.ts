import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, RouterModule, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    RouterLink,
    RouterLinkActive,
    MaterialModule,
    DatePipe
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent implements OnInit, OnDestroy {
  username: string = 'User';
  currentDateTime: Date = new Date();
  private clockSubscription: Subscription | undefined;

  constructor(private router: Router) { }

  ngOnInit() {
    this.clockSubscription = interval(1000).subscribe(() => {
      this.currentDateTime = new Date();
    });
  }

  ngOnDestroy() {
    if (this.clockSubscription) {
      this.clockSubscription.unsubscribe();
    }
  }

  logout(): void {
    localStorage.removeItem('auth_token');
    this.router.navigate(['/']);
  }
}
