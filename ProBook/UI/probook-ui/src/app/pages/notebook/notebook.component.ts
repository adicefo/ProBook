import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { NotebookService } from '../../services/notebook-service';
import { Notebook } from '../../interfaces/notebook-interface';
import { Router } from '@angular/router';
import { User } from '../../interfaces/user-interface';
import { UserService } from '../../services/user-service';

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

  loggedInUser:User|null=null;


  constructor(
    private notebookService: NotebookService,
    private router: Router, 

    private userService:UserService
  ) { }

  ngOnInit(): void {
   this.userService.getCurrentUser().subscribe((res:any)=>{
    this.loggedInUser=res;
    this.loadNotebooks();
   }, (err:any)=>{
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
    // TODO: Navigate to notebook detail page
    console.log('Notebook clicked:', notebook);
    // this.router.navigate(['/notebook', notebook.id]);
  }

  onMenuAction(action: string, notebook: Notebook): void {
    console.log(`${action} action for notebook:`, notebook);
    // TODO: Implement menu actions (edit, delete, share, etc.)
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
