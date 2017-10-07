import { Component, OnInit } from '@angular/core';

import { ICustomer } from './customer'
import { CustomerService } from "./customer.service";

@Component({
    selector: 'customer-list',
    templateUrl: './customer-list.component.html'
})
export class CustomerListComponent implements OnInit {

    public customers: ICustomer[];

    constructor(private _customerService: CustomerService) { }

    ngOnInit(): void {
        this.customers = this._customerService.getAll();
    }

}