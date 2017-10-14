import { Component, OnInit } from '@angular/core';

import { ICustomer } from '../../data-entities/customer'
import { CustomerService } from "../../services/customer.service";

@Component({
    templateUrl: './customer-list.component.html'
})
export class CustomerListComponent implements OnInit {

    public customers: ICustomer[];

    constructor(private _customerService: CustomerService) { }

    ngOnInit(): void {
        this._customerService.getAll().subscribe(result => {
            this.customers = result;
        }, error => console.error(error));
    }

}