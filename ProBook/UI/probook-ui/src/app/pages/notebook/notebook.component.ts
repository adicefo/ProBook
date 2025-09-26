import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';

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
export class NotebookComponent {

}
