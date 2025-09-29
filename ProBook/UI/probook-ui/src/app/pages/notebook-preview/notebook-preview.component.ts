import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { PageService } from '../../services/page-service';
import { NotebookService } from '../../services/notebook-service';
import { Page } from '../../interfaces/page-interface';
import { Notebook } from '../../interfaces/notebook-interface';

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
  error: string | null = null;

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
    return this.currentPageIndex + 1 >= this.pages.length && this.pages.length % 2 === 1;
  }

  canNavigateLeft(): boolean {
    return this.currentPageIndex > 0;
  }

  canNavigateRight(): boolean {
    return this.currentPageIndex + 2 < this.pages.length;
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
    // TODO: Implement create new page functionality
    console.log('Create new page clicked');
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