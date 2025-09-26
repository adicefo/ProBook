import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { Router } from '@angular/router';

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

  constructor(private router: Router) { }

  submit() {
    if (this.loginForm.valid) {
      console.log(this.loginForm.value);
      // In a real app, you would authenticate with a backend here
      localStorage.setItem('auth_token', 'demo_token');
      this.router.navigate(['/app']);
    }
  }

  toggleVisibility() {
    this.isObscured = !this.isObscured;
  }
}
