import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IAddress } from '../shared/models/address';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IOrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl = environment.apiUrl;
  userId: string;

  constructor(private http: HttpClient) {
    
  }


  createOrder(order: IOrderToCreate) {
    console.log(order);
    return this.http.post(this.baseUrl + 'Orders/CreateOrder', order);
  }

  getUserAddress() {
    this.userId = localStorage.getItem('userId');
    return this.http.get(this.baseUrl + 'Account/getalluseraddress/' + this.userId).pipe(
      map((dm: IAddress[]) => {
        console.log(dm);
        return dm;
      })
    )
  }

  getDeliveryMethods() {
    return this.http.get(this.baseUrl + 'Orders/deliveryMethods').pipe(
      map((dm: IDeliveryMethod[]) => {
        return dm.sort((a, b) => b.price - a.price);
      })
    )
  }
}
