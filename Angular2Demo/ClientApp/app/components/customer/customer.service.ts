import { Injectable } from '@angular/core'

import { ICustomer } from './customer'

@Injectable()
export class CustomerService {

    getAll(): ICustomer[] {
        return [
            { id: 1, firstName: 'John', lastName: 'Doe' },
            { id: 2, firstName: 'Homer', lastName: 'Simpson' },
            { id: 3, firstName: 'Marge', lastName: 'Simpson' },
            { id: 4, firstName: 'Betty', lastName: 'White' },
            { id: 5, firstName: 'Bob', lastName: 'Dylan' }
        ];
    }
}