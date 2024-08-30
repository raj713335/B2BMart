import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { SharedModule } from '../shared/shared.module';
import {SlickCarouselModule} from 'ngx-slick-carousel';



@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    SlickCarouselModule
  ],
  exports: [HomeComponent]
})
export class HomeModule { }
