import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";

import { PagedData, ICustomer, IOrder } from '../../data-entities/index'
import { CustomerService, OrderService } from "../../services/index";


@Component({
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.css']
})
export class CustomerDetailsComponent implements OnInit {

    customer: ICustomer;
    isLoading: boolean;
    errorMessage: string;

    constructor(private customerService: CustomerService,
        private orderService: OrderService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {
        let id = +(this.route.snapshot.paramMap.get('id') || -1);
        this.loadData(id);
    }

    loadData(id: number): void {

        if (id === 0) {
            //TODO: handle invalid id
        } else {
            this.isLoading = true;
            this.customerService.getById(id).subscribe(result => {
                this.customer = result;
                this.isLoading = false
            }, error => this.handleError(error));
        }
    }

    handleError(error: any) {
        this.errorMessage = error;
        this.isLoading = false;
    }

}
