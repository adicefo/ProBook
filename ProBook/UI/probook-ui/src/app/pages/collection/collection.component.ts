import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';

@Component({
  selector: 'app-collection',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule
  ],
  templateUrl: './collection.component.html',
  styleUrl: './collection.component.css'
})
export class CollectionComponent {

}
