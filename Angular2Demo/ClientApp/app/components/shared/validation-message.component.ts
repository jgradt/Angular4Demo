import { Component, OnInit, Input } from '@angular/core';
import { AbstractControl } from '@angular/forms'

@Component({
  selector: 'app-validation-message',
  templateUrl: './validation-message.component.html',
  styleUrls: ['./validation-message.component.css']
})
export class ValidationMessageComponent implements OnInit {

    @Input('for') formControl: AbstractControl;
    @Input() messages: { [key: string]: string };
    @Input() propertyName: string;

    ngOnInit(): void {
        console.log('init control validator');
    }

    errMsg(): any {
        if (this.formControl && this.formControl.invalid && this.formControl.errors) {
            let key = Object.keys(this.formControl.errors)[0];
            let pname = this.propertyName || 'Value';
            if (this.messages && this.messages[key]) {
                return this.messages[key];
            } else if (key === 'required') {
                return `${pname} is required`;
            } else if (key === 'minlength') {
                return `${pname} must be at least ${this.formControl.errors[key].requiredLength} characters`;
            } else if (key === 'maxlength') {
                return `${pname} must be no longer than ${this.formControl.errors[key].requiredLength} characters`;
            }

            return 'Error';
        }

        return null;
    }

}
