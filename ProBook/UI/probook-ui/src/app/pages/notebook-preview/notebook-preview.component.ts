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
import { ConfirmDialogComponent, ConfirmDialogData } from '../../utils/confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { SharedNotebookService } from '../../services/sharedNotebook-service';

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
  error: string | null = null;
  snackBar: MatSnackBar = new MatSnackBar();
  isShare: boolean = false;
  sharedNotebookId: number = 0;
  // Comment-related properties
  showCommentPanel: boolean = false;
  selectedPageForComments: Page | null = null;
  comments: CommentModel[] = [];
  loadingComments: boolean = false;
  newCommentText: string = '';
  currentUser: User | null = null;
  pageCommentCounts: Map<number, number> = new Map();
  hasNewComments: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private pageService: PageService,
    private notebookService: NotebookService,
    private commentService: CommentService,
    private userService: UserService,
    private sharedNotebookService: SharedNotebookService,
    private dialog: MatDialog
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
      const openComments = this.route.snapshot.queryParams['openComments'] as string;
      this.sharedNotebookId = this.route.snapshot.queryParams['snId'] as number ?? 0;
      console.log(this.isShare + ":" + this.sharedNotebookId);
      if (notebookId) {
        this.loadNotebook(notebookId);
        this.loadPages(notebookId).then(() => {

          this.loadAllPageCommentCounts();

          if (openComments === 'true' && this.pages.length > 0) {
            this.hasNewComments = true;

            const firstPageWithComments = this.pages.find(p => this.getPageCommentCount(p.id!) > 0);
            if (firstPageWithComments) {
              setTimeout(() => {
                this.openCommentPanel(firstPageWithComments);
              }, 500);
            }
          }
        });
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

  loadPages(notebookId: number): Promise<void> {
    this.loading = true;
    return new Promise((resolve, reject) => {
      this.pageService.getAllPages(notebookId).subscribe({
        next: (pages) => {
          this.pages = pages;
          this.loading = false;
          resolve();
        },
        error: (err) => {
          this.error = 'Failed to load pages';
          this.loading = false;
          console.error('Error loading pages:', err);
          reject(err);
        }
      });
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

  deletePage(page: any): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Confirm Deletion',
        message: 'Are you sure you want to delete this page?',
        entityName: page.title,
        confirmText: 'Delete Page'
      } as ConfirmDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.pageService.delete(page?.id ?? 0).subscribe((res: any) => {
          this.loadPages(this.notebook?.id ?? 0);
          this.snackBar.open('Page deleted successfully', 'Close');

        }, (err: any) => {
          this.snackBar.open('Failed to delete page');
          console.log(err);
        });
      }
    });

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

    //check if the shared notebook id is undefined
    if (this.sharedNotebookId === 0) {
      this.sharedNotebookService.getAll({ fromUserId: this.currentUser?.id }).subscribe({
        next: (result) => {
          console.log(result.result);
          if (result.result?.length && result.result.length > 0) {
            this.sharedNotebookId = result.result![0].id!;
            this.createComment();
          }
          else {
            this.snackBar.open('You are not allowed to comment on this notebook', 'Close', { duration: 2000 });
            return;
          }

        },
        error: (err) => {
          console.error('Error loading shared notebooks:', err);
          this.snackBar.open('Failed to load shared notebook information', 'Close', { duration: 2000 });
        }
      });
    } else {
      this.createComment();
    }
  }

  private createComment(): void {
    if (!this.selectedPageForComments?.id) {
      return;
    }

    console.log("Shared notebook id after :" + this.sharedNotebookId);
    const newComment = {
      content: this.newCommentText,
      pageId: this.selectedPageForComments.id,
      userid: this.currentUser?.id,
      sharedNotebookId: this.sharedNotebookId,
      viewed: false
    };

    console.log("Shared notebook id:" + newComment.sharedNotebookId);

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

  deleteComment(comment: any): void {
    if (!comment.id) return;
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Confirm Deletion',
        message: 'Are you sure you want to delete this comment?',
        entityName: comment.content,
        confirmText: 'Delete Comment'
      } as ConfirmDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.commentService.delete(comment?.id ?? 0).subscribe({
          next: () => {
            this.comments = this.comments.filter(c => c.id !== comment?.id);
            this.loadComments(this.selectedPageForComments?.id ?? 0);
            this.snackBar.open('Comment deleted successfully', 'Close', { duration: 2000 });
          },
          error: (err) => {
            console.error('Error deleting comment:', err);
            this.snackBar.open('Failed to delete comment', 'Close', { duration: 2000 });
          }
        });
      }
    });


  }

  markCommentAsViewed(comment: CommentModel): void {
    if (!comment.id || comment.viewed) return;
    console.log(comment.sharedNotebook?.fromUserId + ":" + this.currentUser?.id);
    if (comment.sharedNotebook?.fromUserId === this.currentUser?.id) {
      this.commentService.updateViewed([comment.id]).subscribe({
        next: () => {
          comment.viewed = true;

        },
        error: (err) => {
          console.error('Error marking comment as viewed:', err);
        }
      });
    }
    else
      comment.viewed = true;

  }

  getUnreadCommentCount(): number {
    return this.comments.filter(c => !c.viewed).length;
  }

  isCommentByCurrentUser(comment: CommentModel): boolean {
    return comment.user?.id === this.currentUser?.id;
  }

  // Load comment counts for all pages
  loadAllPageCommentCounts(): void {
    if (!this.pages || this.pages.length === 0) return;

    this.pages.forEach(page => {
      if (page.id) {
        this.commentService.getAll({ pageId: page.id }).subscribe({
          next: (result) => {
            const unreadCount = result.result?.filter(c =>
              !c.viewed && c.user?.id !== this.currentUser?.id
            ).length || 0;
            this.pageCommentCounts.set(page.id!, unreadCount);
          },
          error: (err) => {
            console.error('Error loading comment count for page:', err);
          }
        });
      }
    });
  }

  getPageCommentCount(pageId: number): number {
    return this.pageCommentCounts.get(pageId) || 0;
  }

  hasPageNewComments(pageId: number | undefined): boolean {
    if (!pageId) return false;
    return this.getPageCommentCount(pageId) > 0;
  }

  getTotalNewCommentsCount(): number {
    let total = 0;
    this.pageCommentCounts.forEach(count => total += count);
    return total;
  }

  navigateToPageWithComments(forward: boolean = true): void {
    const pagesWithComments = this.pages.filter(p => this.hasPageNewComments(p.id));

    if (pagesWithComments.length === 0) {
      this.snackBar.open('No pages with new comments', 'Close', { duration: 2000 });
      return;
    }

    // Find next page with comments
    let targetPage: Page | null = null;

    if (forward) {
      // Find next page with comments after current page
      targetPage = pagesWithComments.find(p =>
        this.pages.indexOf(p) > this.currentPageIndex
      ) || pagesWithComments[0]; // Wrap to first if at end
    } else {
      // Find previous page with comments before current page
      const reversedPages = [...pagesWithComments].reverse();
      targetPage = reversedPages.find(p =>
        this.pages.indexOf(p) < this.currentPageIndex
      ) || pagesWithComments[pagesWithComments.length - 1]; // Wrap to last if at start
    }

    if (targetPage) {
      const targetIndex = this.pages.indexOf(targetPage);
      // Navigate to the page (adjust for two-page spread)
      this.currentPageIndex = targetIndex % 2 === 0 ? targetIndex : targetIndex - 1;
      this.openCommentPanel(targetPage);
    }
  }
}