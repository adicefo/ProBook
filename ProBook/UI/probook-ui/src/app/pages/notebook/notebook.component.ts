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
import { ShareModalComponent } from '../share-modal/share-modal.component';
import { SharedNotebookService } from '../../services/sharedNotebook-service';
import { AddToCollectionComponent } from '../add-to-collection/add-to-collection.component';
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
    private sharedNotebookService: SharedNotebookService,
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
  openShareModal(notebook: Notebook): void {
    const dialogRef = this.dialog.open(ShareModalComponent, {
      width: '600px',
      data: {
        fromUserId: this.loggedInUser?.id,
        notebook: notebook,
      }
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      
      if (result && result.formData) {
        console.log('Sharing notebook with user:', result.user);
        this.sharedNotebookService.create(result.formData).subscribe((res:any)=>{
          this.snackBar.open('Notebook shared successfully','Close',{
            duration:2000
          }); 

        },(err:any)=>{
          this.snackBar.open('Failed to share notebook','Close',{
            duration:2000
          });
          this.snackBar.open('Already shared with this user','Close',{
            duration:2000
          });

        })
        
      }
    });
  }
  openAddNotebookDialog(id?: number, notebook: Notebook | null = null): void {
    const dialogRef = this.dialog.open(AddNotebookComponent, {
      width: '400px',
      data: {
        userId: id,
        notebook: notebook !== null ? notebook : null
      }
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      if (result && !result.data.isEdit) {

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
      else if (result && result.data.isEdit) {

        this.notebookService.updateNotebook(notebook?.id ?? 0, result.formData).subscribe(
          (res: any) => {
            this.snackBar.open('Notebook updated successfully.', 'Close', {
              duration: 2000
            });
            this.loadNotebooks();
          }, (err: any) => {
            this.snackBar.open('Failed to update notebook.', 'Close', {
              duration: 2000
            });
            this.loadNotebooks();
          }
        )
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
    if (action === 'edit') {
      this.openAddNotebookDialog(notebook.userId, notebook);
    }
    if (action === 'share') {
      this.openShareModal(notebook);
    }
    if(action==='add-collection'){
      this.openAddCollectionDialog(notebook);
    }
    // TODO: Implement menu actions (edit, delete, share, etc.)
  }

  openAddCollectionDialog(notebook: Notebook): void {
    const dialogRef = this.dialog.open(AddToCollectionComponent, {
      width: '500px',
      data: {
        notebook: notebook
      }
    });
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
