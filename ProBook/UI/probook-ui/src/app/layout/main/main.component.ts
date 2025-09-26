import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, RouterModule, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { interval, Subscription } from 'rxjs';
import { UserService } from '../../services/user-service';
import { AuthService } from '../../services/auth-service';
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

  constructor(private router: Router, private authService: AuthService, private userService: UserService) { }

  ngOnInit() {
    this.clockSubscription = interval(1000).subscribe(() => {
      this.currentDateTime = new Date();
    });
    this.userService.getCurrentUser().subscribe((res: any) => {
      this.username = res.username;
    }, (err: any) => {
      console.log(err);
    });
  }

  ngOnDestroy() {
    if (this.clockSubscription) {
      this.clockSubscription.unsubscribe();
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
  navigateToProfile():void{

  }
  navigateToSettings():void{
    
  }
}