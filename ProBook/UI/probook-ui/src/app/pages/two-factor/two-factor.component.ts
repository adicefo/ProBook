import { Component } from '@angular/core';
import { Form, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user-service';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-twofactor',
  imports:[
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './two-factor.component.html',
  styleUrls: ['./two-factor.component.css']
})
export class TwoFactorComponent {
  twoFactorForm: FormGroup;
  username: string = '';
  error: string = '';

  constructor(private fb: FormBuilder, private router: Router, private userService: UserService) {
    this.twoFactorForm = this.fb.group({
      code: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]]
    });

    const storedUser = localStorage.getItem('pendingUser');
    if (storedUser) {
      this.username = storedUser;
    }
  }

  verifyCode() {
    if (this.twoFactorForm.invalid) return;

    const code = this.twoFactorForm.value.code;

    const body={
      username: this.username,
      code: code
    };

    this.userService.verifyTwoFactor(body).subscribe({
      next: (res) => {
        console.log(res);
        if(res.message==='Invalid or expired code.'){
          this.error = 'Invalid or expired code. Please try again.';
          this.twoFactorForm.reset();
          return;
        }
        localStorage.removeItem('pendingUser');
        this.router.navigate(['/app']);
      }, 
      error: (err) => {
        this.error = 'Invalid or expired code. Please try again.';
        this.twoFactorForm.reset();
      }
    });
  }
}
