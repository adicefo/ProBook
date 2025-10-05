import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { debounceTime, switchMap, tap } from 'rxjs/operators';
import { CollectionService } from '../../services/collection-service';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Notebook } from '../../interfaces/notebook-interface';

@Component({
  selector: 'app-add-to-collection-dialog',
  templateUrl: './add-to-collection.component.html',
  imports: [CommonModule, MaterialModule, FormsModule, ReactiveFormsModule],
  styleUrls: ['./add-to-collection.component.css']
})
export class AddToCollectionComponent implements OnInit {
  nameFilter = new FormControl('');
  filteredCollections: any[] = [];
  selectedCollection: any = null;
  showDropdown = false;
  loading = false;
  notebook: Notebook | null = null;

  // New collection creation
  showCreateNew = false;
  newCollectionName = new FormControl('');
  newCollectionDescription = new FormControl('');
  creating = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private collectionService: CollectionService,
    private dialogRef: MatDialogRef<AddToCollectionComponent>
  ) {
    this.notebook = data.notebook;
  }

  ngOnInit() {
    this.loadCollections('');

    this.nameFilter.valueChanges
      .pipe(
        debounceTime(300),
        tap(() => {
          this.loading = true;
          this.showDropdown = true;
        }),
        switchMap(value =>
          this.collectionService.getAll({ name: value, userId: this.notebook?.userId }).pipe(
            tap(() => (this.loading = false))
          )
        )
      )
      .subscribe({
        next: (res: any) => (this.filteredCollections = res.result || []),
        error: () => {
          this.loading = false;
          this.filteredCollections = [];
        }
      });
  }

  loadCollections(searchTerm: string) {
    this.loading = true;
    this.collectionService.getAll({ userId: this.notebook?.userId }).subscribe({
      next: (res: any) => {
        console.log(res.result);
        this.filteredCollections = res.items || [];
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.filteredCollections = [];
      }
    });
  }

  onFilterFocus() {
    this.showDropdown = true;
  }

  selectCollection(collection: any) {
    this.selectedCollection = collection;
    this.showDropdown = false;
  }

  removeSelectedCollection() {
    this.selectedCollection = null;
  }

  toggleCreateNew() {
    this.showCreateNew = !this.showCreateNew;
    if (this.showCreateNew) {
      this.showDropdown = false;
      this.newCollectionName.setValue('');
      this.newCollectionDescription.setValue('');
    }
  }

  createNewCollection() {
    const name = this.newCollectionName.value?.trim();
    if (!name) return;

    this.creating = true;
    const newCollection = {
      name: name,
      description: this.newCollectionDescription.value?.trim() || '',
      userId: this.notebook?.userId
    };

    this.collectionService.create(newCollection).subscribe({
      next: (created: any) => {
        this.selectedCollection = created;
        this.showCreateNew = false;
        this.creating = false;
        this.newCollectionName.setValue('');
        this.newCollectionDescription.setValue('');
        this.loadCollections('');
      },
      error: (err) => {
        console.error('Error creating collection:', err);
        this.creating = false;
      }
    });
  }

  cancel() {
    this.dialogRef.close();
  }

  save() {
    if (!this.selectedCollection) return;
    this.dialogRef.close(this.selectedCollection);
  }
}
