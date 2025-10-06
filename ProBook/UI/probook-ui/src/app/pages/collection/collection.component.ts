import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { CollectionService } from '../../services/collection-service';
import { CollectionResponse } from '../../interfaces/collectionResponse';
import { Router } from '@angular/router';
import { User } from '../../interfaces/user-interface';
import { UserService } from '../../services/user-service';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Notebook } from '../../interfaces/notebook-interface';
import { NotebookService } from '../../services/notebook-service';

@Component({
  selector: 'app-collection',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule
  ],
  templateUrl: './collection.component.html',
  styleUrl: './collection.component.css'
})
export class CollectionComponent implements OnInit {
  collections: CollectionResponse[] = [];
  loading = true;
  error: string | null = null;
  loggedInUser: User | null = null;
  showDeleteConfirmation: boolean = false;
  collectionToDelete: CollectionResponse | null = null;

  // Modal for showing notebooks in collection
  showNotebooksModal: boolean = false;
  selectedCollection: CollectionResponse | null = null;
  allNotebooks: Notebook[] = [];
  loadingNotebooks: boolean = false;

  // Pagination state
  collectionCurrentPage: number = 1;
  collectionPageSize: number = 4;

  constructor(
    private collectionService: CollectionService,
    private notebookService: NotebookService,
    private router: Router,
    private userService: UserService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.userService.getCurrentUser().subscribe((res: any) => {
      this.loggedInUser = res;
      this.loadCollections();
    }, (err: any) => {
      this.loadCollections();
    });
  }

  loadCollections(): void {
    this.loading = true;
    this.error = null;

    const userId = this.loggedInUser?.id ?? 0;

    this.collectionService.getCollectionResponse(userId).subscribe({
      next: (collections) => {
        this.collections = collections || [];
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load collections';
        this.loading = false;
        console.error('Error loading collections:', err);
      }
    });
  }

  onCollectionClick(collection: CollectionResponse): void {
    this.selectedCollection = collection;
    this.showNotebooksModal = true;
    this.loadAllNotebooks();
  }

  loadAllNotebooks(): void {
    this.loadingNotebooks = true;
    const userId = this.loggedInUser?.id ?? 0;

    const params = {
      Page: 0,
      PageSize: 1000, 
      UserId: userId
    };

    this.notebookService.getAll(params).subscribe({
      next: (response) => {
        this.allNotebooks = response.result || [];
        this.loadingNotebooks = false;
      },
      error: (err) => {
        console.error('Error loading notebooks:', err);
        this.loadingNotebooks = false;
        this.snackBar.open('Failed to load notebooks', 'Close', { duration: 2000 });
      }
    });
  }

  isNotebookInCollection(notebook: Notebook): boolean {
    if (!this.selectedCollection || !this.selectedCollection.notebooks) {
      return false;
    }
    return this.selectedCollection.notebooks.some(n => n.id === notebook.id);
  }

  addNotebookToCollection(notebook: Notebook): void {
    if (!this.selectedCollection) return;

    const request = {
      notebookId: notebook.id,
      collectionId: this.selectedCollection.id
    };

    this.collectionService.addToCollection(request).subscribe({
      next: (res) => {
        this.snackBar.open('Notebook added to collection successfully', 'Close', { duration: 2000 });
        this.loadCollections();
        if (this.selectedCollection && this.selectedCollection.notebooks) {
          this.selectedCollection.notebooks.push(notebook);
        }
      },
      error: (err) => {
        this.snackBar.open('Failed to add notebook to collection', 'Close', { duration: 2000 });
        console.error('Error adding notebook to collection:', err);
      }
    });
  }

  removeNotebookFromCollection(notebook: Notebook): void {
    if (!this.selectedCollection) return;

    const request = {
      notebookId: notebook.id,
      collectionId: this.selectedCollection.id
    };

    this.collectionService.removeFromCollection(request).subscribe({
      next: (res) => {
        this.snackBar.open('Notebook removed from collection successfully', 'Close', { duration: 2000 });
        this.loadCollections();
        if (this.selectedCollection && this.selectedCollection.notebooks) {
          this.selectedCollection.notebooks = this.selectedCollection.notebooks.filter(n => n.id !== notebook.id);
        }
      },
      error: (err) => {
        this.snackBar.open('Failed to remove notebook from collection', 'Close', { duration: 2000 });
        console.error('Error removing notebook from collection:', err);
      }
    });
  }

  closeNotebooksModal(): void {
    this.showNotebooksModal = false;
    this.selectedCollection = null;
    this.allNotebooks = [];
  }

  openCreateCollectionDialog(): void {
    this.snackBar.open('Create Collection dialog - To be implemented', 'Close', { duration: 2000 });
  }

  onMenuAction(action: string, collection: CollectionResponse): void {
    console.log(`${action} action for collection:`, collection);
    if (event) {
      event.stopPropagation();
    }
    if (action === 'delete') {
      this.showDeleteConfirmation = true;
      this.collectionToDelete = collection;
    }
    if (action === 'edit') {
      this.snackBar.open('Edit functionality - To be implemented', 'Close', { duration: 2000 });
    }
  }

  cancelDelete(): void {
    this.showDeleteConfirmation = false;
    this.collectionToDelete = null;
  }

  deleteCollection(): void {
    if (!this.collectionToDelete || !this.collectionToDelete.id) return;

    this.collectionService.delete(this.collectionToDelete.id).subscribe({
      next: (res) => {
        this.loadCollections();
        this.snackBar.open('Collection deleted successfully', 'Close', { duration: 2000 });
        this.showDeleteConfirmation = false;
        this.collectionToDelete = null;
      },
      error: (err) => {
        this.snackBar.open('Failed to delete collection', 'Close', { duration: 2000 });
        console.error('Error deleting collection:', err);
      }
    });
  }

  formatDate(date: Date | undefined): string {
    if (!date) return '';
    return new Date(date).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  }

  getCollectionCoverColor(index: number): string {
    const colors = [
      '#6366f1', // indigo
      '#8b5cf6', // violet
      '#06b6d4', // cyan
      '#10b981', // emerald
      '#f59e0b', // amber
      '#ef4444', // red
      '#84cc16',  // lime
      '#ffea00'  //yellow
    ];
    return colors[index % colors.length];
  }

  get paginatedCollections(): CollectionResponse[] {
    const startIndex = (this.collectionCurrentPage - 1) * this.collectionPageSize;
    const endIndex = startIndex + this.collectionPageSize;
    return this.collections.slice(startIndex, endIndex);
  }

  get totalPages(): number {
    return Math.ceil(this.collections.length / this.collectionPageSize);
  }

  previousPage(): void {
    if (this.collectionCurrentPage > 1) {
      this.collectionCurrentPage--;
    }
  }

  nextPage(): void {
    if (this.collectionCurrentPage < this.totalPages) {
      this.collectionCurrentPage++;
    }
  }

  isLastPage(): boolean {
    return this.collectionCurrentPage === this.totalPages || this.totalPages === 0;
  }

  isFirstPage(): boolean {
    return this.collectionCurrentPage === 1;
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages || page === this.collectionCurrentPage) {
      return;
    }
    this.collectionCurrentPage = page;
  }

  getPageNumbers(): number[] {
    const pages: number[] = [];
    const maxPagesToShow = 5;
    if (this.totalPages <= maxPagesToShow) {
      for (let i = 1; i <= this.totalPages; i++) {
        pages.push(i);
      }
    } else {
      const halfWindow = Math.floor(maxPagesToShow / 2);
      let startPage = Math.max(1, this.collectionCurrentPage - halfWindow);
      let endPage = Math.min(this.totalPages, this.collectionCurrentPage + halfWindow);

      if (this.collectionCurrentPage <= halfWindow) {
        endPage = maxPagesToShow;
      } else if (this.collectionCurrentPage >= this.totalPages - halfWindow) {
        startPage = this.totalPages - maxPagesToShow + 1;
      }
      for (let i = startPage; i <= endPage; i++) {
        pages.push(i);
      }
    }
    return pages;
  }
}
