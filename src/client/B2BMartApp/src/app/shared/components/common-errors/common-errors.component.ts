import { Component, Input, OnInit } from '@angular/core';
import {FormControl} from '@angular/forms';

@Component({
  selector: 'app-common-errors',
  templateUrl: './common-errors.component.html',
  styleUrls: ['./common-errors.component.scss']
})
export class CommonErrorsComponent implements OnInit {

  @Input() control: FormControl;
  @Input() for: FormControl;

  constructor() { }

  ngOnInit(): void {
  }

  config(key, validatorValue): object {
    const _config = {
      required: 'This field is required.',
      pattern: `Please enter valid ${this.for}.`,
      minlength: `Enter minimum ${validatorValue && validatorValue.minlength ? validatorValue.minlength.requiredLength : ''} characters.`,
      space: `Password cannot contain space`
    };
    return _config[key];
  }
  get errorMessage(): string {
    if (this.control.dirty && this.control.touched) {
      for (const propertyName in this.control.errors) {
        if (this.control.errors.hasOwnProperty(propertyName)) {
          return this.control.errors['label'] || this.config(propertyName, this.control.errors);
        }
      }
    }
    return '';
  }


}
