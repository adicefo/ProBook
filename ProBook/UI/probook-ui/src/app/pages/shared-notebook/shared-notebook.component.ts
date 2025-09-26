import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';

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
export class SharedNotebookComponent {

}
