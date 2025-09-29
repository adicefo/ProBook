import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MaterialModule } from '../../material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-notebook-dialog',
  templateUrl: './add-notebook.component.html',
  imports: [
    CommonModule,
    MaterialModule, FormsModule, ReactiveFormsModule
  ],
  styleUrls: ['./add-notebook.component.css']
})
export class AddNotebookComponent implements OnInit {
  notebookForm: FormGroup;
  loading = false;
  selectedFile: File | null = null;
  selectedFileName: string = '';
  userId: number = 0;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddNotebookComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.notebookForm = this.fb.group({
      name: ['', Validators.required],
      description: ['']
    });
  }
  ngOnInit(): void {
    this.userId = this.data.userId;
  }


  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.selectedFileName = file.name;
    }
  }

  save() {
    if (this.notebookForm.invalid) return;

    this.loading = true;

    const formData = new FormData();
    formData.append('name', this.notebookForm.get('name')?.value);
    formData.append('description', this.notebookForm.get('description')?.value);
    formData.append('userId', this.data.userId);
    if (this.selectedFile) {
      formData.append('file', this.selectedFile);
    }

    setTimeout(() => {
      this.loading = false;
      this.dialogRef.close({
        formData: formData,
        data: {
          name: this.notebookForm.get('name')?.value,
          description: this.notebookForm.get('description')?.value,
          file: this.selectedFile,
          userId: this.userId
        }
      });
    }, 1000);
  }

  cancel() {
    this.dialogRef.close();
  }
}
