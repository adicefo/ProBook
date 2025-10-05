import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { SharedNotebookService } from '../../services/sharedNotebook-service';
import { UserService } from '../../services/user-service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedNotebook } from '../../interfaces/sharedNotebook-interface';
import { User } from '../../interfaces/user-interface';
import { forkJoin } from 'rxjs';
import { CommentService } from '../../services/comment-service';

@Component({
  selector: 'app-shared-notebook',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule
  ],
  templateUrl: './shared-notebook.component.html',
  styleUrl: './shared-notebook.component.css'
})
export class SharedNotebookComponent implements OnInit {
  sharedWithMeNotebooks: SharedNotebook[] = [];
  sharedByMeNotebooks: SharedNotebook[] = [];
  currentUser: User | null = null;
  loading = true;
  loadingSharedByMe = true;
  error: string | null = null;
  selectedTab: 'received' | 'shared' = 'received';
  sharedNotebookToDelete: SharedNotebook | null = null;
  showDeleteConfirmation = false;
  commentCounts: Map<number, number> = new Map();
  commentIds: number[] = [];

  // PAGINATION STATE FOR SHARED WITH ME"
 
  sharedWithMeCurrentPage: number = 1;
  sharedWithMePageSize: number = 4;
  sharedWithMeTotalItems: number = 0;
  sharedWithMeTotalPages: number = 0;

  // PAGINATION  FOR SHARED BY ME 
  sharedByMeCurrentPage: number = 1;
  sharedByMePageSize: number = 4;
  sharedByMeTotalItems: number = 0;
  sharedByMeTotalPages: number = 0;

  constructor(
    private sharedNotebookService: SharedNotebookService,
    private userService: UserService,
    private commentService: CommentService,
    private router: Router,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.userService.getCurrentUser().subscribe({
      next: (user) => {
        this.currentUser = user;
        this.loadSharedNotebooks();
      },
      error: (err) => {
        this.error = 'Failed to load user information';
        this.loading = false;
        this.loadingSharedByMe = false;
        console.error('Error loading current user:', err);
      }
    });
  }


  loadSharedWithMeNotebooks(): void {
    if (!this.currentUser?.id) return;

    this.loading = true;

    
    const params = {
      ToUserId: this.currentUser.id,
      Page: this.sharedWithMeCurrentPage - 1,
      PageSize: this.sharedWithMePageSize
    };

    this.sharedNotebookService.getAll(params).subscribe({
      next: (result: any) => {
        console.log("Shared with me:", result);
        
        this.sharedWithMeNotebooks = result.result || [];


        this.sharedWithMeTotalItems = result.count || 0;

        this.sharedWithMeTotalPages = Math.ceil(this.sharedWithMeTotalItems / this.sharedWithMePageSize);

        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load shared notebooks';
        this.loading = false;
        console.error('Error loading shared with me:', err);
      }
    });
  }


  loadSharedByMeNotebooks(): void {
    if (!this.currentUser?.id) return;

    this.loadingSharedByMe = true;

    const params = {
      FromUserId: this.currentUser.id,
      Page: this.sharedByMeCurrentPage - 1,
      PageSize: this.sharedByMePageSize
    };

    this.sharedNotebookService.getAll(params).subscribe({
      next: (result: any) => {
        console.log("Shared by me:", result);
        this.sharedByMeNotebooks = result.result || [];

        this.sharedByMeTotalItems = result.count || 0;

        this.sharedByMeTotalPages = Math.ceil(this.sharedByMeTotalItems / this.sharedByMePageSize);

        this.loadingSharedByMe = false;

        this.loadCommentCounts();
      },
      error: (err) => {
        this.loadingSharedByMe = false;
        console.error('Error loading shared by me:', err);
      }
    });
  }


  loadSharedNotebooks(): void {
    this.loadSharedWithMeNotebooks();
    this.loadSharedByMeNotebooks();
  }

  loadCommentCounts(): void {
    if (this.sharedByMeNotebooks.length === 0) return;
    this.sharedByMeNotebooks.forEach(element => {
      this.sharedNotebookService.getNumberOfComments(element.id!, this.currentUser?.id!).subscribe({
        next: (result) => {

          this.commentCounts.set(element.id!, result.item1);
          this.commentIds = result.item2;
        },
        error: (err) => {
          console.error('Error loading comment counts:', err);
        }
      });
    });


  }

  getCommentCount(sharedNotebookId: number | undefined): number {
    if (!sharedNotebookId) return 0;
    return this.commentCounts.get(sharedNotebookId) || 0;
  }

  onBadgeClick(sharedNotebook: SharedNotebook, event: Event): void {
    event.stopPropagation();

    if (!sharedNotebook.id) return;

    // Clear the badge count
    this.commentCounts.set(sharedNotebook.id, 0);

    // TODO: Backend logic will be implemented later to mark comments as read
    // For now, just show a message

    this.router.navigate(['/app/notebook/', sharedNotebook.notebook?.id], {
      queryParams: { isShare: false, snId: sharedNotebook.id }
    });

  }

  onNotebookClick(sharedNotebook: SharedNotebook): void {
    if (sharedNotebook.notebook?.id) {
      const hasNewComments = this.getCommentCount(sharedNotebook.id) > 0;
      this.router.navigate(['/app/notebook', sharedNotebook.notebook.id], {
        queryParams: {
          isShare: true,
          openComments: hasNewComments ? 'true' : 'false',
          snId: sharedNotebook?.id
        }
      });
    }
  }

  onMenuAction(action: string, sharedNotebook: SharedNotebook, event: Event): void {
    event.stopPropagation();

    if (action === 'comment') {
      this.snackBar.open('Comment feature coming soon!', 'Close', { duration: 2000 });
    }
  }

  onDeleteShared(sharedNotebook: SharedNotebook, event: Event): void {
    event.stopPropagation();
    this.sharedNotebookToDelete = sharedNotebook;
    this.showDeleteConfirmation = true;
  }

  confirmDeleteShared(): void {
    if (!this.sharedNotebookToDelete?.id) return;

    this.sharedNotebookService.delete(this.sharedNotebookToDelete.id).subscribe({
      next: () => {
        this.snackBar.open('Shared notebook removed successfully', 'Close', { duration: 2000 });
        this.loadSharedNotebooks();
        this.cancelDelete();
      },
      error: (err) => {
        this.snackBar.open('Failed to remove shared notebook', 'Close', { duration: 2000 });
        console.error('Error deleting shared notebook:', err);
      }
    });
  }

  cancelDelete(): void {
    this.showDeleteConfirmation = false;
    this.sharedNotebookToDelete = null;
  }

  formatDate(date: Date | undefined): string {
    if (!date) return '';
    return new Date(date).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  }

  getNotebookCoverColor(index: number): string {
    const purpleShades = [
      '#9333ea', // purple-600
      '#a855f7', // purple-500
      '#c084fc', // purple-400
      '#7c3aed', // violet-600
      '#8b5cf6', // violet-500
      '#a78bfa', // violet-400
      '#6d28d9', // purple-700
      '#7e22ce'  // purple-800
    ];
    return purpleShades[index % purpleShades.length];
  }

  changeTab(tab: 'received' | 'shared'): void {
    this.selectedTab = tab;
  }

  // PAGINATION FOR SHARED WITH ME
  goToSharedWithMePage(page: number): void {
    if (page < 1 || page > this.sharedWithMeTotalPages || page === this.sharedWithMeCurrentPage) {
      return; // Invalid page or already on this page
    }

    this.sharedWithMeCurrentPage = page;
    this.loadSharedWithMeNotebooks();
  }

  
  nextSharedWithMePage(): void {
    if (this.sharedWithMeCurrentPage < this.sharedWithMeTotalPages) {
      this.sharedWithMeCurrentPage++;
      this.loadSharedWithMeNotebooks();
    }
  }

 
  previousSharedWithMePage(): void {
    if (this.sharedWithMeCurrentPage > 1) {
      this.sharedWithMeCurrentPage--;
      this.loadSharedWithMeNotebooks();
    }
  }


  isSharedWithMeFirstPage(): boolean {
    return this.sharedWithMeCurrentPage === 1;
  }

 
  isSharedWithMeLastPage(): boolean {
    return this.sharedWithMeCurrentPage === this.sharedWithMeTotalPages || this.sharedWithMeTotalPages === 0;
  }


  getSharedWithMePageNumbers(): number[] {
    const pages: number[] = [];
    const maxPagesToShow = 5;

    if (this.sharedWithMeTotalPages <= maxPagesToShow) {
      // Show all pages if total is small
      for (let i = 1; i <= this.sharedWithMeTotalPages; i++) {
        pages.push(i);
      }
    } else {
      const halfWindow = Math.floor(maxPagesToShow / 2);
      let startPage = Math.max(1, this.sharedWithMeCurrentPage - halfWindow);
      let endPage = Math.min(this.sharedWithMeTotalPages, this.sharedWithMeCurrentPage + halfWindow);

      if (this.sharedWithMeCurrentPage <= halfWindow) {
        endPage = maxPagesToShow;
      } else if (this.sharedWithMeCurrentPage >= this.sharedWithMeTotalPages - halfWindow) {
        startPage = this.sharedWithMeTotalPages - maxPagesToShow + 1;
      }

      for (let i = startPage; i <= endPage; i++) {
        pages.push(i);
      }
    }

    return pages;
  }

 // PAGINATION FOR SHARED BY ME
  goToSharedByMePage(page: number): void {
    if (page < 1 || page > this.sharedByMeTotalPages || page === this.sharedByMeCurrentPage) {
      return;
    }

    this.sharedByMeCurrentPage = page;
    this.loadSharedByMeNotebooks();
  }

 
  nextSharedByMePage(): void {
    if (this.sharedByMeCurrentPage < this.sharedByMeTotalPages) {
      this.sharedByMeCurrentPage++;
      this.loadSharedByMeNotebooks();
    }
  }

  
  previousSharedByMePage(): void {
    if (this.sharedByMeCurrentPage > 1) {
      this.sharedByMeCurrentPage--;
      this.loadSharedByMeNotebooks();
    }
  }

  
  isSharedByMeFirstPage(): boolean {
    return this.sharedByMeCurrentPage === 1;
  }


  isSharedByMeLastPage(): boolean {
    return this.sharedByMeCurrentPage === this.sharedByMeTotalPages || this.sharedByMeTotalPages === 0;
  }


  getSharedByMePageNumbers(): number[] {
    const pages: number[] = [];
    const maxPagesToShow = 5;

    if (this.sharedByMeTotalPages <= maxPagesToShow) {
      for (let i = 1; i <= this.sharedByMeTotalPages; i++) {
        pages.push(i);
      }
    } else {
      const halfWindow = Math.floor(maxPagesToShow / 2);
      let startPage = Math.max(1, this.sharedByMeCurrentPage - halfWindow);
      let endPage = Math.min(this.sharedByMeTotalPages, this.sharedByMeCurrentPage + halfWindow);

      if (this.sharedByMeCurrentPage <= halfWindow) {
        endPage = maxPagesToShow;
      } else if (this.sharedByMeCurrentPage >= this.sharedByMeTotalPages - halfWindow) {
        startPage = this.sharedByMeTotalPages - maxPagesToShow + 1;
      }

      for (let i = startPage; i <= endPage; i++) {
        pages.push(i);
      }
    }

    return pages;
  }
}
