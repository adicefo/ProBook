import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { NotebookService } from '../../services/notebook-service';
import { Notebook } from '../../interfaces/notebook-interface';
import { Router } from '@angular/router';

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

  constructor(
    private notebookService: NotebookService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadNotebooks();
  }

  loadNotebooks(): void {
    this.loading = true;
    this.error = null;

   
    const userId = 1;

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

  // Generate a random notebook cover color for visual variety
  getNotebookCoverColor(index: number): string {
    const colors = [
      '#6366f1', // indigo
      '#8b5cf6', // violet
      '#06b6d4', // cyan
      '#10b981', // emerald
      '#f59e0b', // amber
      '#ef4444', // red
      '#ec4899', // pink
      '#84cc16'  // lime
    ];
    return colors[index % colors.length];
  }
}
