import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { BasketService } from '../../basket/basket.service';
import { IBasket } from '../../shared/models/basket';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/shared/models/user';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  basket$: Observable<IBasket>;
  searchForm: FormGroup;
  currentUser$: Observable<IUser>;
  userName: string = null;

  constructor(private accountService: AccountService,
    private formBuilder: FormBuilder,
    public router: Router,
    private basketService: BasketService
  ) {
    this.userName = localStorage.getItem('userName');
    
  }

  ngOnInit(): void {
    
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.accountService.currentUser$;
    this.searchForm = this.formBuilder.group({
      globalSearchText: ['', [Validators.required]]
    });
    //this.userName = sessionStorage.getItem('userName');
    //console.log(this.userName);
  }
  loginRedirect() {
    this.router.navigate(['/account/login']);
  }
  logout() {
    this.accountService.logout();
  }


}
