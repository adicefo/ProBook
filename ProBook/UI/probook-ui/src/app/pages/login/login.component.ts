import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { Router } from '@angular/router';
import { UserService } from '../../services/user-service';
import { AuthService } from '../../services/auth-service';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });
  isObscured: boolean = true;

  error: string | null = null;

  constructor(private router: Router, private authService: AuthService, private userService: UserService) { }

  submit() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value.username, this.loginForm.value.password);
      try {
        this.userService.getAllUsers().subscribe(
          (res: any) => {
            this.router.navigate(['/app']);
          },
          (err: any) => {
            this.error = "Invalid username or password";
          }
        );

      }
      catch (err: any) {
        this.error = "Invalid username or password";
        alert("Invalid username or password");
      }

    }
  }

  toggleVisibility() {
    this.isObscured = !this.isObscured;
  }
}
