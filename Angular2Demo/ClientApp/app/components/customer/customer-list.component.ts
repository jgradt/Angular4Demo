import { Component } from '@angular/core';

import { Customer } from './customer'

@Component({
    selector: 'customer',
    templateUrl: './customer-list.component.html'
})
export class CustomerListComponent {
    public customers: Customer[];

    constructor() {
        this.customers = [
            { id: 1, firstName: 'John', lastName: 'Doe' },
            { id: 2, firstName: 'Homer', lastName: 'Simpson' },
            { id: 3, firstName: 'Marge', lastName: 'Simpson' },
            { id: 4, firstName: 'Betty', lastName: 'White' },
            { id: 5, firstName: 'Bob', lastName: 'Dylan' }
        ];
    }
}