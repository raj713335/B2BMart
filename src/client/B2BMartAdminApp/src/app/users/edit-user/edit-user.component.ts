import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from '../users.component';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {

  editUserForm!: FormGroup;

  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any) {
    const { userDetails } = data;
    this.editUserForm = this.fb.group({
      firstName: [userDetails.firstName, [Validators.required, Validators.minLength(3)]],
      lastName: [userDetails.lastName, [Validators.required, Validators.minLength(3)]],
      email: [userDetails.email, [Validators.required, Validators.email]],
      username: [userDetails.username, [Validators.required, Validators.minLength(3)]],
      password: [userDetails.password, [Validators.required, Validators.minLength(3)]],
      userRole: [userDetails.userRole, [Validators.required]],
      phoneNumber: [userDetails.phoneNumber, [Validators.required, Validators.minLength(8), Validators.maxLength(15)]],
    })
  }

  ngOnInit(): void {

  }

  onSubmit() {
    console.warn(this.editUserForm.value);
  }
}
