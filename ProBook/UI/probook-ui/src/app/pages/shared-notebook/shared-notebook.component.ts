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
  
  constructor(
    private sharedNotebookService: SharedNotebookService,
    private userService: UserService,
    private router: Router,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    
  }

  loadSharedNotebooks(): void {
    if (!this.currentUser?.id) return;

    this.loading = true;
    this.sharedNotebookService.getAll({ toUserId: this.currentUser.id }).subscribe({
      next: (result) => {
        this.sharedWithMeNotebooks = result.result || [];
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load shared notebooks';
        this.loading = false;
        console.error('Error loading shared with me:', err);
      }
    });

    this.loadingSharedByMe = true;
    this.sharedNotebookService.getAll({ fromUserId: this.currentUser.id }).subscribe({
      next: (result) => {
        this.sharedByMeNotebooks = result.result || [];
        this.loadingSharedByMe = false;
        // Load comment counts for each shared notebook
        this.loadCommentCounts();
      },
      error: (err) => {
        this.loadingSharedByMe = false;
        console.error('Error loading shared by me:', err);
      }
    });

  }

  loadCommentCounts(): void {
    if (this.sharedByMeNotebooks.length === 0) return;
    this.sharedByMeNotebooks.forEach(element => {
      this.sharedNotebookService.getNumberOfComments(element.id!).subscribe({
        next: (result) => {
          this.commentCounts.set(element.id!, result.item1);
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
    this.snackBar.open('Comments marked as viewed', 'Close', { duration: 2000 });
  }

  onNotebookClick(sharedNotebook: SharedNotebook): void {
    if (sharedNotebook.notebook?.id) {
      this.router.navigate(['/app/notebook', sharedNotebook.notebook.id], {
        queryParams: { isShare: true }
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
}
