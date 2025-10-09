import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MaterialModule } from '../../../material.module';
import { UserService } from '../../../services/user-service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { AuthService } from '../../../services/auth-service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-change-password-modal',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MaterialModule,
        MatSnackBarModule
    ],
    templateUrl: './change-password-modal.component.html',
    styleUrl: './change-password-modal.component.css'
})
export class ChangePasswordModalComponent {
    passwordForm: FormGroup;
    isSubmitting = false;
    hidePassword = true;
    hideConfirmPassword = true;
    userId: number | null = null;

    constructor(
        private fb: FormBuilder,
        private userService: UserService,
        private authService: AuthService,
        private router: Router,
        private snackBar: MatSnackBar,
        public dialogRef: MatDialogRef<ChangePasswordModalComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { userId?: number }
    ) {
        this.passwordForm = this.fb.group({
            password: ['', [
                Validators.required,
                Validators.minLength(3),

            ]],
            confirmPassword: ['', [Validators.required]]
        }, {
            validators: this.passwordMatchValidator
        });

        if (this.data && this.data.userId) {
            this.userId = this.data.userId;
        } else {
            this.loadCurrentUser();
        }
    }

    private loadCurrentUser(): void {
        this.userService.getCurrentUser().subscribe({
            next: (user) => {
                if (user && user.id) {
                    this.userId = user.id;
                } else {
                    this.showNotification('Unable to load user information', 'error');
                    this.dialogRef.close();
                }
            },
            error: (error) => {
                console.error('Error loading current user:', error);
                this.showNotification('Unable to load user information', 'error');
                this.dialogRef.close();
            }
        });
    }



    // Custom validator to check if passwords match
    passwordMatchValidator(group: AbstractControl): ValidationErrors | null {
        const password = group.get('password')?.value;
        const confirmPassword = group.get('confirmPassword')?.value;

        return password === confirmPassword ? null : { passwordMismatch: true };
    }

    onSubmit(): void {
        if (!this.userId) {
            this.showNotification('User ID not available. Please try again.', 'error');
            return;
        }

        if (this.passwordForm.valid) {
            this.isSubmitting = true;

            const body = {
                password: this.passwordForm.value.password,
                passwordConfirm: this.passwordForm.value.confirmPassword
            };

            this.userService.updatePassword(this.userId, body).subscribe({
                next: () => {
                    const username = this.authService.getCurrentUsername();
                    if (username) {
                        this.authService.updateToken(username, this.passwordForm.value.password);
                    }

                    this.showNotification('Password updated successfully!', 'success');
                    this.isSubmitting = false;
                    this.dialogRef.close('success');
                },
                error: (error) => {
                    console.error('Error updating password:', error);
                    this.showNotification('Failed to update password', 'error');
                    this.isSubmitting = false;
                }
            });
        } else {
            this.markFormGroupTouched(this.passwordForm);
        }
    }

    onCancel(): void {
        this.dialogRef.close();
    }

    private markFormGroupTouched(formGroup: FormGroup): void {
        Object.keys(formGroup.controls).forEach(key => {
            const control = formGroup.get(key);
            control?.markAsTouched();
        });
    }

    private showNotification(message: string, type: 'success' | 'error'): void {
        this.snackBar.open(message, 'Close', {
            duration: 3000,
            horizontalPosition: 'right',
            verticalPosition: 'top',
            panelClass: [`snackbar-${type}`]
        });
    }

    getPasswordErrorMessage(): string {
        const control = this.passwordForm.get('password');
        if (control?.hasError('required')) {
            return 'Password is required';
        }
        if (control?.hasError('minlength')) {
            return 'Password must be at least 8 characters long';
        }
        if (control?.hasError('weakPassword')) {
            return 'Password must contain uppercase, lowercase, number and special character';
        }
        return '';
    }

    getConfirmPasswordErrorMessage(): string {
        const control = this.passwordForm.get('confirmPassword');
        if (control?.hasError('required')) {
            return 'Please confirm your password';
        }
        if (this.passwordForm.hasError('passwordMismatch')) {
            return 'Passwords do not match';
        }
        return '';
    }


    // Helper methods for password requirements

}

