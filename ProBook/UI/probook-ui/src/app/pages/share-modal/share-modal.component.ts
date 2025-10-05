import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MaterialModule } from '../../material.module';
import { CommonModule } from '@angular/common';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { UserService } from '../../services/user-service';
import { User } from '../../interfaces/user-interface';
import { Notebook } from '../../interfaces/notebook-interface';

@Component({
  selector: 'app-share-modal',
  imports: [CommonModule,
    MaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './share-modal.component.html',
  styleUrl: './share-modal.component.css'
})
export class ShareModalComponent implements OnInit {
  shareForm: FormGroup;
  nameFilter = new FormControl('');
  surnameFilter = new FormControl('');
  loading = false;

  allUsers: User[] = [];
  filteredUsers: User[] = [];
  selectedUser: User | null = null;
  notebook: Notebook;
  showDropdown: boolean = false;
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ShareModalComponent>,
    private userService: UserService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.notebook = data.notebook;
    this.shareForm = this.fb.group({
      fromUserId: [data.fromUserId, Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadUsers();

    this.nameFilter.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged()
      )
      .subscribe(() => this.applyFilters());

    this.surnameFilter.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged()
      )
      .subscribe(() => this.applyFilters());
  }

  loadUsers(): void {
    this.loading = true;
    this.userService.getAllUsers().subscribe({
      next: (response) => {
        this.allUsers = response.result || [];
        this.filteredUsers = [...this.allUsers];
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading users:', err);
        this.loading = false;
      }
    });
  }

  applyFilters(): void {
    const nameValue = this.nameFilter.value?.toLowerCase().trim() || '';
    const surnameValue = this.surnameFilter.value?.toLowerCase().trim() || '';

    if (!nameValue && !surnameValue) {
      this.filteredUsers = [...this.allUsers];
    } else {
      this.filteredUsers = this.allUsers.filter(user => {
        const matchesName = !nameValue || user.name?.toLowerCase().includes(nameValue);
        const matchesSurname = !surnameValue || user.surname?.toLowerCase().includes(surnameValue);
        return matchesName && matchesSurname;
      });
    }


    this.showDropdown = Boolean(nameValue || surnameValue) && this.filteredUsers.length > 0;
  }

  clearFilters(): void {
    this.nameFilter.setValue('');
    this.surnameFilter.setValue('');
    this.showDropdown = false;
  }

  selectUser(user: User): void {
    this.selectedUser = user;
    this.showDropdown = false;
    // this.clearFilters();
  }

  removeSelectedUser(): void {
    this.selectedUser = null;
  }

  onFilterFocus(): void {
    if ((this.nameFilter.value || this.surnameFilter.value) && this.filteredUsers.length > 0) {
      this.showDropdown = true;
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }

  save(): void {
    if (this.selectUser === null) return;
    this.loading = true;
    const insertData = {
      toUserId: this.selectedUser?.id,
      notebookId: this.notebook?.id,
      fromUserId: this.shareForm.get('fromUserId')?.value,
      isForEdit: false
    };


    console.log(insertData);


    setTimeout(() => {
      this.loading = false;
      this.dialogRef.close({
        formData: insertData,
        data: {
          toUserId: this.selectedUser?.id,
          notebookId: this.notebook.id,
          fromUserId: this.shareForm.get('fromUserId')?.value,
          isForEdit: false
        }
      });
    }, 1000);

  }
}
