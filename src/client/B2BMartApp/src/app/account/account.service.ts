import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

import { IAddress } from '../shared/models/address';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<IUser>(1);
  currentUser$ = this.currentUserSource.asObservable();
  userId: string;

  constructor(public http: HttpClient, private router: Router) { }
  
  //loadCurrentUser(token: string) {
  //  if (token == null) {
  //    this.currentUserSource.next(null);
  //    return of(null);
  //  }

  //  let headers = new HttpHeaders();
  //  headers = headers.set('Authorization', `Bearer ${token}`);

  //  return this.http.get(this.baseUrl + 'Account', { headers }).pipe(
  //    map((user: IUser) => {
  //      if (user) {
  //        localStorage.setItem('userName', user.username);
  //        localStorage.setItem('token', user.token);
  //        this.currentUserSource.next(user);
  //      }
  //    })
  //  )
  //}

  login(values: any) {
    return this.http.post(this.baseUrl + 'Account/login', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('userId', user.userId.toString());
          localStorage.setItem('userName', user.username);
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    )
  }

  register(values: any) {
    return this.http.post(this.baseUrl + 'Account/register', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('userId', user.userId.toString());
          localStorage.setItem('userName', user.username);
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    )
  }

  logout() {
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.http.get(this.baseUrl + 'Account/emailexists?email=' + email);
  }

  getUserAddress() {
    this.userId = localStorage.getItem('userId');
    return this.http.get<IAddress>(this.baseUrl + 'Account/getalluseraddress/'+ this.userId);
  }

  updateUserAddress(address: IAddress) {
    return this.http.put<IAddress>(this.baseUrl + 'Account/updateAddress', address);
  }
}
