import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';
import { IAddress } from 'src/app/shared/models/address';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutForm: FormGroup;
  addressMethods: IAddress[];
  userId: string;

  constructor(private checkoutService: CheckoutService, private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.userId = localStorage.getItem('userId');
    this.checkoutService.getUserAddress().subscribe((dm: IAddress[]) => {
      this.addressMethods = dm;
    }, error => {
      console.log(error);
    })
  }

  saveUserAddress() {
    this.accountService.updateUserAddress(this.checkoutForm.get('addressForm').value).subscribe((address: IAddress) => {
      this.toastr.success('Address saved');
      this.checkoutForm.get('addressForm').reset(address);
    }, error => {
      this.toastr.error(error.message);
      console.log(error);
    })
  }

}
