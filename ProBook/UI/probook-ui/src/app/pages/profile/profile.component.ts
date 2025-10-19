import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../../material.module';
import { UserService } from '../../services/user-service';
import { User } from '../../interfaces/user-interface';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ChangePasswordModalComponent } from './change-password-modal/change-password-modal.component';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule,
    MatSnackBarModule
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  currentUser: User | null = null;
  isLoading = false;
  isSaving = false;

  genderOptions = ['Male', 'Female'];

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {
    this.profileForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      surname: ['', [Validators.required, Validators.minLength(2)]],
      username: ['', [Validators.required, Validators.minLength(3)]],
      email: [{ value: '', }],
      telephoneNumber: ['', [Validators.pattern(/^[+]?[\d\s\-()]+$/)]],
      gender: ['']
    });
  }

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.isLoading = true;
    this.userService.getCurrentUser().subscribe({
      next: (user: User) => {
        this.currentUser = user;
        this.profileForm.patchValue({
          name: user.name || '',
          surname: user.surname || '',
          username: user.username || '',
          email: user.email || '',
          telephoneNumber: user.telephoneNumber || '',
          gender: user.gender || ''
        });
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading user profile:', error);
        this.showNotification('Failed to load profile', 'error');
        this.isLoading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.profileForm.valid && this.currentUser?.id) {
      this.isSaving = true;

      const updatedUser: User = {
        name: this.profileForm.value.name,
        surname: this.profileForm.value.surname,
        email: this.profileForm.value.email,
        telephoneNumber: this.profileForm.value.telephoneNumber,
        gender: this.profileForm.value.gender
      };

      this.userService.updateUser(this.currentUser.id, updatedUser).subscribe({
        next: (user: User) => {
          this.currentUser = user;
          this.showNotification('Profile updated successfully!', 'success');
          localStorage.setItem('username', user?.username ?? '');
          this.isSaving = false;
        },
        error: (error) => {
          console.error('Error updating profile:', error);
          this.showNotification('Failed to update profile', 'error');
          this.isSaving = false;
        }
      });
    } else {
      this.markFormGroupTouched(this.profileForm);
      this.showNotification('Please fill in all required fields correctly', 'error');
    }
  }

  openChangePasswordModal(): void {
    if (!this.currentUser?.id) {
      this.showNotification('Please wait while user data is loading...', 'error');

      this.userService.getCurrentUser().subscribe({
        next: (user: User) => {
          this.currentUser = user;
          if (user?.id) {
            this.openPasswordModal(user.id);
          } else {
            this.showNotification('Unable to load user information', 'error');
          }
        },
        error: (error) => {
          console.error('Error loading user:', error);
          this.showNotification('Failed to load user data', 'error');
        }
      });
      return;
    }

    this.openPasswordModal(this.currentUser.id);
  }

  private openPasswordModal(userId: number): void {
    const dialogRef = this.dialog.open(ChangePasswordModalComponent, {
      width: '450px',
      data: { userId: userId },
      disableClose: false
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'success') {
        this.showNotification('Password changed successfully!', 'success');
      }
    });
  }

  resetForm(): void {
    if (this.currentUser) {
      this.profileForm.patchValue({
        name: this.currentUser.name || '',
        surname: this.currentUser.surname || '',
        username: this.currentUser.username || '',
        telephoneNumber: this.currentUser.telephoneNumber || '',
        gender: this.currentUser.gender || ''
      });
      this.showNotification('Changes discarded', 'info');
    }
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }

  private showNotification(message: string, type: 'success' | 'error' | 'info'): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
      panelClass: [`snackbar-${type}`]
    });
  }

  getErrorMessage(fieldName: string): string {
    const control = this.profileForm.get(fieldName);
    if (control?.hasError('required')) {
      return `${fieldName.charAt(0).toUpperCase() + fieldName.slice(1)} is required`;
    }
    if (control?.hasError('minlength')) {
      const minLength = control.errors?.['minlength'].requiredLength;
      return `Minimum length is ${minLength} characters`;
    }
    if (control?.hasError('pattern')) {
      return 'Invalid format';
    }
    return '';
  }
}
