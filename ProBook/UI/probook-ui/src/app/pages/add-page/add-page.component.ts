import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { PageService } from '../../services/page-service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { NotebookService } from '../../services/notebook-service';
import { Notebook } from '../../interfaces/notebook-interface';

@Component({
  selector: 'app-add-page',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule
  ],
  templateUrl: './add-page.component.html',
  styleUrl: './add-page.component.css'
})
export class AddPageComponent implements OnInit {
  notebookId: number = 0;

  pageForm: FormGroup;
  loading = false;
  selectedFile: File | null = null;
  selectedFileName: string = '';
  contentType: 'text' | 'image' | 'both' = 'text';
  notebook: Notebook | null = null;

  constructor(
    private fb: FormBuilder,
    private pageService: PageService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private notebookService: NotebookService
  ) {
    this.pageForm = this.fb.group({
      title: ['', Validators.required],
      content: ['']
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.notebookId = +params['id'];
      this.notebookService.getNotebookById(this.notebookId).subscribe((notebook: Notebook) => {
        this.notebook = notebook;
      });
    });
    console.log(this.notebookId);
 

  }

  onContentTypeChange(type: 'text' | 'image' | 'both'): void {
    this.contentType = type;


    const contentControl = this.pageForm.get('content');

    if (type === 'image') {
      contentControl?.clearValidators();
    } else {
      contentControl?.setValidators([Validators.required]);
    }

    contentControl?.updateValueAndValidity();
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.selectedFileName = file.name;
    }
  }

  isFormValid(): boolean | undefined {
    const titleValid = this.pageForm.get('title')?.valid;

    switch (this.contentType) {
      case 'text':
        return titleValid && this.pageForm.get('content')?.valid;
      case 'image':
        return titleValid && !!this.selectedFile;
      case 'both':
        return titleValid && this.pageForm.get('content')?.valid && !!this.selectedFile;
      default:
        return false;
    }
  }

  save(): void {
    if (!this.isFormValid()) return;

    this.loading = true;


    const formData = new FormData();
    formData.append('Title', this.pageForm.get('title')?.value || '');
    formData.append('Content', this.pageForm.get('content')?.value || '');
    formData.append('NotebookId', this.notebookId.toString());

    if (this.selectedFile) {
      formData.append('File', this.selectedFile);
    }

    this.pageService.create(formData).subscribe({
      next: (response) => {
        this.loading = false;
        this.snackBar.open('Page created successfully!', 'Close', {
          duration: 3000
        });
        // Navigate back to notebook preview
        this.router.navigate(['/app/notebook', this.notebookId]);
      },
      error: (error) => {
        this.loading = false;
        console.error('Error creating page:', error);
        this.snackBar.open('Failed to create page!', 'Close', {
          duration: 3000
        });
      }
    });
  }

  cancel(): void {
    // Navigate back to notebook preview
    this.router.navigate(['/app/notebook', this.notebookId]);
  }

  removeFile(): void {
    this.selectedFile = null;
    this.selectedFileName = '';
  }
}
