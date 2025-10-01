import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { PageService } from '../../services/page-service';
import { NotebookService } from '../../services/notebook-service';
import { Page } from '../../interfaces/page-interface';
import { Notebook } from '../../interfaces/notebook-interface';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-notebook-preview',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule
  ],
  templateUrl: './notebook-preview.component.html',
  styleUrl: './notebook-preview.component.css'
})
export class NotebookPreviewComponent implements OnInit {
  notebook: Notebook | null = null;
  pages: Page[] = [];
  currentPageIndex = 0;
  loading = true;
  pageToDelete:Page|null=null;
  error: string | null = null;
  showDeleteConfirmation:boolean=false;
  snackBar:MatSnackBar=new MatSnackBar();
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private pageService: PageService,
    private notebookService: NotebookService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const notebookId = +params['id'];
      if (notebookId) {
        this.loadNotebook(notebookId);
        this.loadPages(notebookId);
      }
    });
  }

  loadNotebook(id: number): void {
    this.notebookService.getNotebookById(id).subscribe({
      next: (notebook) => {
        this.notebook = notebook;
      },
      error: (err) => {
        this.error = 'Failed to load notebook';
        console.error('Error loading notebook:', err);
      }
    });
  }

  loadPages(notebookId: number): void {
    this.loading = true;
    this.pageService.getAllPages(notebookId).subscribe({
      next: (pages) => {
        this.pages = pages;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load pages';
        this.loading = false;
        console.error('Error loading pages:', err);
      }
    });
  }

  get leftPage(): Page | null {
    const leftIndex = this.currentPageIndex;
    return leftIndex < this.pages.length ? this.pages[leftIndex] : null;
  }

  get rightPage(): Page | null {
    const rightIndex = this.currentPageIndex + 1;
    return rightIndex < this.pages.length ? this.pages[rightIndex] : null;
  }

  get showCreatePagePlaceholder(): boolean {
    // Show on the right side if there's an odd number of pages and we're at the last pair
    return this.currentPageIndex + 1 >= this.pages.length;
  }

  canNavigateLeft(): boolean {
    return this.currentPageIndex > 0;
  }

  canNavigateRight(): boolean {
    return this.currentPageIndex + 2 <= this.pages.length;
  }

  navigateLeft(): void {
    if (this.canNavigateLeft()) {
      this.currentPageIndex -= 2;
    }
  }

  navigateRight(): void {
    if (this.canNavigateRight()) {
      this.currentPageIndex += 2;
    }
  }

  goBack(): void {
    this.router.navigate(['/app/notebooks']);
  }

  onCreateNewPage(): void {
    if (this.notebook?.id) {
      this.router.navigate(['/app/notebook', this.notebook.id, 'add-page']);
    }
  }

  onMenuAction(action: string, page: Page): void {
    console.log(`${action} action for notebook:`, page);
    if (event) {
      event.stopPropagation();
    }
    if(action==='delete'){
      this.showDeleteConfirmation=true;
      this.pageToDelete=page;
    }
   
    // TODO: Implement menu actions (edit, delete, share, etc.)
  }
  cancelDelete():void{
    this.showDeleteConfirmation=false;
    this.pageToDelete=null;
  }
  deletePage():void{
    this.pageService.delete(this.pageToDelete?.id ?? 0).subscribe((res: any) => {
      this.loadPages(this.notebook?.id ?? 0);
      this.snackBar.open('Page deleted successfully', 'Close');
      this.showDeleteConfirmation = false;
      this.pageToDelete = null;
    }, (err: any) => {
      this.snackBar.open('Failed to delete page');
      console.log(err);
    })
  }
  formatDate(date: Date | undefined): string {
    if (!date) return '';
    return new Date(date).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  min(a: number, b: number): number {
    return Math.min(a, b);
  }
}