import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl = environment.apiUrl;
  userId: string;

  constructor(private http: HttpClient) { }

  getOrdersForUser() {
    this.userId = localStorage.getItem('userId');
    return this.http.get(this.baseUrl + 'Orders/GetAllUserOrder/' + this.userId);
  }

  getOrderDetailed(id: number) {
    this.userId = localStorage.getItem('userId');
    return this.http.get(this.baseUrl + 'Orders/GetUserOrder/' + this.userId +'/'+ id);
  }
}
