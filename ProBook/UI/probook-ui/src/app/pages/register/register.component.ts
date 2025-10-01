import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MaterialModule } from '../../material.module';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user-service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm:FormGroup=new FormGroup({
    name:new FormControl('',[Validators.required]),
    surname:new FormControl('',[Validators.required]),
    username:new FormControl('',[Validators.required]),
    email:new FormControl('',[Validators.required]),
    password:new FormControl('',[Validators.required]),
    passwordConfirm:new FormControl('',[Validators.required]),
    telephoneNumber:new FormControl(''),
    gender:new FormControl(''),
  });
  isObscured:boolean=true;
  
  error: string | null = null;
  snackBar: MatSnackBar = new MatSnackBar();
  constructor(private router:Router,private userService:UserService){
    
  }
 
  submit(){
    if(this.registerForm.invalid) return;
    if(this.registerForm.get('password')?.value !== this.registerForm.get('passwordConfirm')?.value){
      this.error = "Password and password confirm do not match";
      return;
    }
    
    this.userService.createUser(this.registerForm.value).subscribe({
      next: (response) => {
        this.snackBar.open('User created successfully','Close',{
          duration:3000
        });
        this.router.navigate(['/login']);
      },
      error: (error) => {
        this.snackBar.open('User creation failed','Close',{
          duration:3000
        });
        console.log(error);
      }
    });
  }
  toggleVisibility(){
this.isObscured=!this.isObscured;
  }
}
