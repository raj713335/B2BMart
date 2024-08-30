import {HttpHeaders, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import {FormArray, FormControl, FormGroup} from '@angular/forms';
@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor() { }

  public validateAllFormFields(form: FormGroup) {
    const keys = Object.keys(form.controls);
    keys.forEach((field: any) => {
      const control = form.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
        control.markAsDirty({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      } else if (control instanceof FormArray) {
        control.markAsTouched({ onlySelf: true });
        control.markAsDirty({ onlySelf: true });
      //  (<FormArray>control).controls.forEach((element: FormGroup) => {
      //    this.validateAllFormFields(element);
      //  });
      }
    });
  }
  getUserData(): any {
    return JSON.parse(localStorage.getItem('user') || '{}');
  }
  getHeaders(): any {
    if (this.getUserData()) {
      return new HttpHeaders()
        .set('Authorization', `Bearer ${this.getUserData().token}`);
    } else {
      this.logOut();
    }
  }
  logOut(): void {
    const body = {};
    const params = new HttpParams({ fromObject: body });
    localStorage.clear();
  }
}
