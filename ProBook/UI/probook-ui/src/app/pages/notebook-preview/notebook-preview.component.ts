import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { FormsModule } from '@angular/forms';
import { PageService } from '../../services/page-service';
import { NotebookService } from '../../services/notebook-service';
import { CommentService } from '../../services/comment-service';
import { UserService } from '../../services/user-service';
import { Page } from '../../interfaces/page-interface';
import { Notebook } from '../../interfaces/notebook-interface';
import { Comment as CommentModel } from '../../interfaces/comment-interface';
import { User } from '../../interfaces/user-interface';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-notebook-preview',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule
  ],
  templateUrl: './notebook-preview.component.html',
  styleUrl: './notebook-preview.component.css'
})
export class NotebookPreviewComponent implements OnInit {
  notebook: Notebook | null = null;
  pages: Page[] = [];
  currentPageIndex = 0;
  loading = true;
  pageToDelete: Page | null = null;
  error: string | null = null;
  showDeleteConfirmation: boolean = false;
  snackBar: MatSnackBar = new MatSnackBar();
  isShare: boolean = false;

  // Comment-related properties
  showCommentPanel: boolean = false;
  selectedPageForComments: Page | null = null;
  comments: CommentModel[] = [];
  loadingComments: boolean = false;
  newCommentText: string = '';
  currentUser: User | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private pageService: PageService,
    private notebookService: NotebookService,
    private commentService: CommentService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    // Load current user
    this.userService.getCurrentUser().subscribe({
      next: (user) => {
        this.currentUser = user;
      },
      error: (err) => {
        console.error('Error loading current user:', err);
      }
    });

    this.route.params.subscribe(params => {
      const notebookId = +params['id'];
      this.isShare = this.route.snapshot.queryParams['isShare'] as boolean;
      console.log(this.isShare);
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
    if (action === 'delete') {
      this.showDeleteConfirmation = true;
      this.pageToDelete = page;
    } else if (action === 'edit') {
      this.editPage(page);
    }
    else if (action === 'comment') {
      this.openCommentPanel(page);
    }

  }

  editPage(page: Page): void {
    if (this.notebook?.id && page.id) {
      this.router.navigate(['/app/notebook', this.notebook.id, 'edit-page', page.id]);
    }
  }
  cancelDelete(): void {
    this.showDeleteConfirmation = false;
    this.pageToDelete = null;
  }
  deletePage(): void {
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

  // Comment-related methods
  openCommentPanel(page: Page): void {
    this.selectedPageForComments = page;
    this.showCommentPanel = true;
    this.loadComments(page.id!);
  }

  closeCommentPanel(): void {
    this.showCommentPanel = false;
    this.selectedPageForComments = null;
    this.comments = [];
    this.newCommentText = '';
  }

  loadComments(pageId: number): void {
    this.loadingComments = true;
    this.commentService.getAll({ pageId }).subscribe({
      next: (result) => {
        this.comments = result.result || [];
        this.loadingComments = false;
      },
      error: (err) => {
        console.error('Error loading comments:', err);
        this.loadingComments = false;
        this.snackBar.open('Failed to load comments', 'Close', { duration: 2000 });
      }
    });
  }

  addComment(): void {
    if (!this.newCommentText.trim() || !this.selectedPageForComments?.id) {
      return;
    }

    const newComment = {
      content: this.newCommentText,
      pageId: this.selectedPageForComments.id,
      userid: this.currentUser?.id,
      viewed: false
    };

    this.commentService.create(newComment).subscribe({
      next: (comment) => {
        this.comments.push(comment);
        this.newCommentText = '';
        this.snackBar.open('Comment added successfully', 'Close', { duration: 2000 });
      },
      error: (err) => {
        console.error('Error adding comment:', err);
        this.snackBar.open('Failed to add comment', 'Close', { duration: 2000 });
      }
    });
  }

  deleteComment(commentId: number): void {
    if (!commentId) return;

    this.commentService.delete(commentId).subscribe({
      next: () => {
        this.comments = this.comments.filter(c => c.id !== commentId);
        this.snackBar.open('Comment deleted successfully', 'Close', { duration: 2000 });
      },
      error: (err) => {
        console.error('Error deleting comment:', err);
        this.snackBar.open('Failed to delete comment', 'Close', { duration: 2000 });
      }
    });
  }

  markCommentAsViewed(comment: CommentModel): void {
    if (!comment.id || comment.viewed||(this.isShare && this.currentUser?.id === comment.user?.id)) return;

    this.commentService.updateViewed([comment.id]).subscribe({
      next: () => {
        comment.viewed = true;
      },
      error: (err) => { 
        console.error('Error marking comment as viewed:', err);
      }
    });
  }

  getUnreadCommentCount(): number {
    return this.comments.filter(c => !c.viewed).length;
  }

  isCommentByCurrentUser(comment: CommentModel): boolean {
    return comment.userid === this.currentUser?.id;
  }
}