import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { PageService } from '../../services/page-service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { NotebookService } from '../../services/notebook-service';
import { Notebook } from '../../interfaces/notebook-interface';
import { Page } from '../../interfaces/page-interface';

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
  @Input() page: Page | null = null; // Optional input for edit mode

  notebookId: number = 0;
  isEditMode: boolean = false;

  pageForm: FormGroup;
  loading = false;
  selectedFile: File | null = null;
  selectedFileName: string = '';
  contentType: 'text' | 'image' | 'both' = 'text';
  notebook: Notebook | null = null;
  existingImageUrl: string | null = null;

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
      const pageId = params['pageId'];

      this.notebookService.getNotebookById(this.notebookId).subscribe((notebook: Notebook) => {
        this.notebook = notebook;
      });

      this.isEditMode = !!this.page || !!pageId;

      if (this.isEditMode) {
        if (this.page) {
          this.populateFormForEdit();
        } else if (pageId) {
          this.loadPageForEdit(+pageId);
        }
      }
    });

    console.log(this.notebookId);
  }

  loadPageForEdit(pageId: number): void {
    this.pageService.getById(pageId).subscribe({
      next: (page) => {
        this.page = page;
        this.populateFormForEdit();
      },
      error: (error) => {
        console.error('Error loading page for edit:', error);
        this.snackBar.open('Failed to load page for editing!', 'Close', {
          duration: 3000
        });
        this.router.navigate(['/app/notebook', this.notebookId]);
      }
    });
  }

  populateFormForEdit(): void {
    if (!this.page) return;

    this.pageForm.patchValue({
      title: this.page.title || '',
      content: this.page.content || ''
    });

    if (this.page.imageUrl) {
      this.existingImageUrl = this.page.imageUrl;
    }

    if (this.page.content && this.page.imageUrl) {
      this.contentType = 'both';
    } else if (this.page.imageUrl) {
      this.contentType = 'image';
    } else {
      this.contentType = 'text';
    }

    this.onContentTypeChange(this.contentType);
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
        return titleValid && (!!this.selectedFile || (this.isEditMode && !!this.existingImageUrl));
      case 'both':
        return titleValid && this.pageForm.get('content')?.valid && (!!this.selectedFile || (this.isEditMode && !!this.existingImageUrl));
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

    if (this.isEditMode && this.page?.id) {
      this.pageService.update(this.page.id, formData).subscribe({
        next: (response) => {
          this.loading = false;
          this.snackBar.open('Page updated successfully!', 'Close', {
            duration: 3000
          });
          this.router.navigate(['/app/notebook', this.notebookId]);
        },
        error: (error) => {
          this.loading = false;
          console.error('Error updating page:', error);
          this.snackBar.open('Failed to update page!', 'Close', {
            duration: 3000
          });
        }
      });
    } else {
      this.pageService.create(formData).subscribe({
        next: (response) => {
          this.loading = false;
          this.snackBar.open('Page created successfully!', 'Close', {
            duration: 3000
          });
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
  }

  cancel(): void {
    this.router.navigate(['/app/notebook', this.notebookId]);
  }

  removeFile(): void {
    this.selectedFile = null;
    this.selectedFileName = '';
  }

  removeExistingImage(): void {
    this.existingImageUrl = null;
  }
}
