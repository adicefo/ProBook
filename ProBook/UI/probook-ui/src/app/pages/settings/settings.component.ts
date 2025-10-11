import { Component, OnInit } from '@angular/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SharedNotebook } from '../../interfaces/sharedNotebook-interface';
import { User } from '../../interfaces/user-interface';
import { UserService } from '../../services/user-service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedNotebookService } from '../../services/sharedNotebook-service';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
@Component({
  selector: 'app-settings',
  standalone:true,
 imports: [CommonModule,MaterialModule,
    
  ],
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
})
export class SettingsComponent implements OnInit {
 
  loading:boolean=false;
  sharedNotebooks:SharedNotebook[]=[];

  currentUser?:User;
  error:string='';

  constructor(
    private userService:UserService,
    private snackBar: MatSnackBar,
    private sharedNotebookService:SharedNotebookService
  ){

  }

  ngOnInit():void{
    this.userService.getCurrentUser().subscribe({
      next: (user) => {
        this.currentUser = user;
        this.loadSharedNotebooks();
      },
      error: (err) => {
        this.error = 'Failed to load user information';
        this.loading = false;
        console.error('Error loading current user:', err);
      }
    });
  }
  
  loadSharedNotebooks():void{
    if(!this.currentUser) return;
    this.loading=true;

    const params={
      fromUserId:this.currentUser?.id??0
    };

    this.sharedNotebookService.getAll(params).subscribe(
      (res:any)=>{
        this.sharedNotebooks=res.result || [];
        this.loading=false;
      }
    )
  }

  onEditPermissionChange(notebook:SharedNotebook):void{
    this.sharedNotebookService.update(notebook.id??0,{isForEdit:notebook.isForEdit}).subscribe({
      next: (res) => {
        this.snackBar.open('Permission updated successfully', 'Close', { duration: 3000 });
      },
      error:(err)=>{
        this.snackBar.open('Failed to update permission', 'Close', { duration: 3000 });
      }
    })
  }

}
