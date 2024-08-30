import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Patterns } from 'src/app/shared/constants/pattern';
import { CommonService } from 'src/app/shared/services/common.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  showPassword = false;
  submitted = false;
  returnUrl: string;
 
  constructor(
    private formBuilder: FormBuilder,
    public _common: CommonService,
    private _accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || 'shop';
    this.initForm();
  }
  initForm(): void {
    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }
  toggle(): void {
    this.showPassword = !this.showPassword;
  }
  login() {
    //this.submitted = true;
    //if (this.loginForm.invalid) {
    //  this.submitted = false;
    //  return this._common.validateAllFormFields(this.loginForm);
    //}
    //const creds = {
    //  username: this.loginForm.value.userName,
    //  password: this.loginForm.value.password,
    //};
    ///*const url = '/Account/login';*/
    ////this._account.authenticate(creds, url).subscribe(({ response }) => {
    ////  this.submitted = false;
    ////  if (response.status === 200) {

    ////  }
    ////});
    //console.log(this.loginForm.value);
    //this._account.login(this.loginForm.value).subscribe(() => {
    //  this.router.navigateByUrl(this.returnUrl);
    //}, error => {
    //  console.log(error);
    //})
    this._accountService.login(this.loginForm.value).subscribe(() => {
      this.router.navigateByUrl(this.returnUrl);
    }, error => {
      this.toastr.error(error.errors);
    })
  }
}
