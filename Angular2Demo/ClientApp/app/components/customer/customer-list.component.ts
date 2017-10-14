import { Component, OnInit } from '@angular/core';

import { ICustomer } from '../../data-entities/customer'
import { CustomerService } from "../../services/customer.service";

@Component({
    templateUrl: './customer-list.component.html'
})
export class CustomerListComponent implements OnInit {

    customers: ICustomer[];
    isLoading: boolean;

    constructor(private _customerService: CustomerService) { }

    ngOnInit(): void {
        this.isLoading = true;

        this._customerService.getAll().subscribe(result => {
            this.customers = result;
            this.isLoading = false;
        }, error => console.error(error));
    }

}