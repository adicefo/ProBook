import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MaterialModule } from '../../material.module';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-collection',
  imports: [
    CommonModule,
    MaterialModule, FormsModule, ReactiveFormsModule
  ],
  templateUrl: './add-collection.component.html',
  styleUrl: './add-collection.component.css'
})
export class AddCollectionComponent implements OnInit {
  collectionForm:FormGroup;
  userId?:number;
  loading = false;

  constructor(private fb:FormBuilder,private dialogRef:MatDialogRef<AddCollectionComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any
  ){
    this.collectionForm=this.fb.group({
      name:[this.data.collection!==null?this.data.collection?.name:'',Validators.required],
      description:[this.data.collection!==null?this.data.collection?.description:'']
    });

  }
  ngOnInit(): void {
      this.userId=this.data?.userId??0;
  }
  
  cancel():void{
    this.dialogRef.close();
  }

  save():void{
    if(this.collectionForm.invalid) return;

    this.loading=true;
 
    const formData={
      name:this.collectionForm.get('name')?.value,
      description:this.collectionForm.get('description')?.value,
      userId:this.userId
    };
    setTimeout(()=>{
      this.loading=false;
      this.dialogRef.close({
        formData:formData,
        data:{
          name:this.collectionForm.get('name')?.value,
          description:this.collectionForm.get('description')?.value,
          userId:this.userId,
        }
      });
    },1000);
  }

}
