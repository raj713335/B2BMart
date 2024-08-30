import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  submitted = false;
  registerForm: FormGroup;
  errors: string[];
  showPassword = false;
 
  constructor(private fb: FormBuilder, private accountService: AccountService, private toastr: ToastrService, private router: Router) { }
  
  //get f() {
  //  return this.registerForm.controls;
  //}
 
  ngOnInit(): void {
    this.createRegisterForm();
    
  }
  createRegisterForm() {
    this.registerForm = this.fb.group({
      userName: [null, [Validators.required]],
      emailId: [null,
        [Validators.required, Validators
          .pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        /*[this.validateEmailNotTaken()]*/
      ],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      phoneNumber: [null, [Validators.required]],
      password: [null, Validators.pattern('^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$')]
    });
  }

  onSubmit() {

    const data = {
      "userName": this.registerForm.value.userName,
      "emailId": this.registerForm.value.emailId,
      "firstName": this.registerForm.value.firstName,
      "lastName": this.registerForm.value.lastName,
      "phoneNumber": this.registerForm.value.phoneNumber,
      "password": this.registerForm.value.password,
      "userType": 2
    }

    //console.log(this.registerForm.value);
    //console.log(data);
    this.accountService.register(data).subscribe(response => {
      this.router.navigateByUrl('/shop');
    }, error => {
      /*console.log(error);*/
      this.toastr.error(error.errors);
      this.errors = error.errors;
    })
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService.checkEmailExists(control.value).pipe(
            map(res => {
              return res ? { emailExists: true } : null;
            })
          );
        })
      )
    }
    
    //const data = {
    //  "userName": this.registerForm.value.full_name,
    //  "emailId": this.registerForm.value.email,
    //  "firstName": this.registerForm.value.full_name.split(' ')[0],
    //  "lastName": this.registerForm.value.full_name.split(' ')[1],
    //  "phoneNumber": this.registerForm.value.contact,
    //  "password": this.registerForm.value.password,
    //  "userType": 2
    //}
    
    //const url = '/Account/register';
    //this._account.authenticate(data, url).subscribe(({ response }) => {
    //  this.submitted = false;
    //  if (response.status === 200) {
        
    //  }
    //});
  }
  toggle() {
    this.showPassword = !this.showPassword;
  }
}
