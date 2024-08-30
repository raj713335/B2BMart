import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  homeImages = [
  '../assets/images/ecom2.jpg',
  '../assets/images/ecom3.jpg',
  '../assets/images/ecom4.jpg',
  '../assets/images/ecom5.png',
  '../assets/images/ecom6.jpg',];
 
  slideConfig = {
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false,
    autoplay: true,
    speed: 150,
    fade: true,
    infinite: true,
    cssEase: 'linear',
    dots: true,
  };
  products = [{
    "productName": "Fastdry Active Training Jacket",
    "description": "Men Blue Washed Cropped Denim Trucker Jacket",
    "pictureUrl": "https://assets.ajio.com/medias/sys_master/root/20210415/q0Oy/60786004f997dd7b64b32985/-1117Wx1400H-441120188-jetblack-MODEL2.jpg",
    "productBrand": "Roadster",
    "price": 100.00,
  }, {
    "productName": "Fastdry Active Training Jacket",
    "description": "Men Blue Washed Cropped Denim Trucker Jacket",
    "pictureUrl": "https://assets.ajio.com/medias/sys_master/root/h09/hcd/12085139111966/-1117Wx1400H-440794191-mediumblue-MODEL.jpg",
    "productBrand": "Roadster",
    "price": 990.00,
  },{
    "productName": "Fastdry Active Training Jacket",
    "description": "Men Blue Washed Cropped Denim Trucker Jacket",
    "pictureUrl": "https://assets.ajio.com/medias/sys_master/root/hb5/hf3/14231513661470/-473Wx593H-460478350-black-MODEL.jpg",
    "productBrand": "Roadster",
    "price": 880.00,
  },{
    "productName": "Fastdry Active Training Jacket",
    "description": "Men Blue Washed Cropped Denim Trucker Jacket",
    "pictureUrl": "https://assets.ajio.com/medias/sys_master/root/hb5/hf3/14231513661470/-473Wx593H-460478350-black-MODEL.jpg",
    "productBrand": "Roadster",
    "price": 658.00,
  },{
    "productName": "Fastdry Active Training Jacket",
    "description": "Men Blue Washed Cropped Denim Trucker Jacket",
    "pictureUrl": "https://assets.ajio.com/medias/sys_master/root/20210415/q0Oy/60786004f997dd7b64b32985/-1117Wx1400H-441120188-jetblack-MODEL2.jpg",
    "productBrand": "Roadster",
    "price": 999.00,
  }];
  constructor(
    public router: Router,
  ) { }

  ngOnInit(): void {
  }
  loginRedirect() {
    this.router.navigate(['/account/login']);
  }
  customerServices(type) {
    this.router.navigate([`/public/${type}`]);
    
    
  }
}
