import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { NotebookService } from '../../services/notebook-service';
import { Notebook } from '../../interfaces/notebook-interface';
import { Router } from '@angular/router';
import { User } from '../../interfaces/user-interface';
import { UserService } from '../../services/user-service';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddNotebookComponent } from '../add-notebook/add-notebook.component';
@Component({
  selector: 'app-notebook',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule
  ],
  templateUrl: './notebook.component.html',
  styleUrl: './notebook.component.css'
})
export class NotebookComponent implements OnInit {
  notebooks: Notebook[] = [];
  loading = true;
  error: string | null = null;
  showDeleteConfirmation: boolean = false;
  loggedInUser: User | null = null;
  notebookToDelete: Notebook | null = null;

  constructor(
    private notebookService: NotebookService,
    private router: Router,

    private userService: UserService,
    private dialog: MatDialog, private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.userService.getCurrentUser().subscribe((res: any) => {
      this.loggedInUser = res;
      this.loadNotebooks();
    }, (err: any) => {
      this.loadNotebooks();
    });
  }

  loadNotebooks(): void {
    this.loading = true;
    this.error = null;


    const userId = this.loggedInUser?.id ?? 0;

    this.notebookService.getAllNotebooks(userId).subscribe({
      next: (notebooks) => {
        this.notebooks = notebooks;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load notebooks';
        this.loading = false;
        console.error('Error loading notebooks:', err);
      }
    });
  }
  onNotebookClick(notebook: Notebook): void {
    if (notebook.id) {
      this.router.navigate(['/app/notebook', notebook.id]);
    }
  }
  openAddNotebookDialog(id?: number): void {
    const dialogRef = this.dialog.open(AddNotebookComponent, {
      width: '400px',
      data: { userId: id }
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      if (result) {
        console.log('Notebook data:', result);
        this.notebookService.createNotebook(result.formData).subscribe((res: any) => {
          this.snackBar.open('Notebook created successfully!', 'Close', {
            duration: 3000
          });
          this.loadNotebooks();
        }, (err: any) => {
          this.snackBar.open('Failed to create notebook', 'Close', {
            duration: 3000
          });
        })

      }
    });
  }


  onMenuAction(action: string, notebook: Notebook): void {
    console.log(`${action} action for notebook:`, notebook);
    if (event) {
      event.stopPropagation();
    }
    if (action === 'delete') {
      this.showDeleteConfirmation = true;
      this.notebookToDelete = notebook;
    }
    // TODO: Implement menu actions (edit, delete, share, etc.)
  }

  cancelDelete(): void {
    this.showDeleteConfirmation = false;
    this.notebookToDelete = null;
  }
  deleteNotebook(): void {
    this.notebookService.deleteNotebook(this.notebookToDelete?.id ?? 0).subscribe((res: any) => {
      this.loadNotebooks();
      this.snackBar.open('Notebook deleted successfully', 'Close');
      this.showDeleteConfirmation = false;
      this.notebookToDelete = null;
    }, (err: any) => {
      this.snackBar.open('Failed to delete notebook');
      console.log(err);
    })
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
}
