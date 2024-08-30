import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from './users.component';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  users: User[] = [
    {
      id: 'string',
      firstName: 'string',
      lastName: 'string',
      email: 'string',
      address: 'string',
      role: 'string',
      isActive: true
    }
  ];

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>('https://jsonplaceholder.typicode.com/users');
  }

  addUser(user: User) {
    this.users.push(user);
  }

  editUser() {

  }
}
