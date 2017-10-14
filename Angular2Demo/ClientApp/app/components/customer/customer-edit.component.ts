import { Component, OnInit } from '@angular/core'
import { ActivatedRoute } from "@angular/router";

import { ICustomer } from '../../data-entities/customer'
import { CustomerService } from "../../services/customer.service";

@Component({
    templateUrl: './customer-edit.component.html'
})
export class CustomerEditComponent implements OnInit {

    public customer: ICustomer;

    constructor(private _customerService: CustomerService, private _route: ActivatedRoute) {}

    ngOnInit(): void {

        let id = +(this._route.snapshot.paramMap.get('id') || -1);

        // TODO: what do we display if id = -1?
        this._customerService.getById(id).subscribe(result => {
            this.customer = result;
        }, error => console.error(error));
    }

}