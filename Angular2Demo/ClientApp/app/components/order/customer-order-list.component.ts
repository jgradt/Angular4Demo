import { Component, OnInit, Input } from '@angular/core';

import { PagedData, IOrder } from '../../data-entities/index'
import { OrderService } from "../../services/index";

@Component({
  selector: 'app-customer-order-list',
  templateUrl: './customer-order-list.component.html',
  styleUrls: ['./customer-order-list.component.css']
})
export class CustomerOrderListComponent implements OnInit {

    @Input() customerId: number;
    data: PagedData<IOrder>;
    totalPages: number = 0; 
    currentPage: number = 1;
    isLoading: boolean;

    constructor(private orderService: OrderService) { }

    ngOnInit() {
        if (this.customerId) {
            this.loadData(this.customerId);
        }
    }

    loadData(customerId: number): void {
        this.orderService.getByCustomerIdPaged(customerId, this.currentPage - 1, 5).subscribe(result => {
            this.data = result;
            this.currentPage = result.pageIndex + 1;
            this.isLoading = false;
        }, error => this.handleError(error));
    }

    pageChanged(event: any): void {

        this.currentPage = event.page;
        this.loadData(this.customerId);
    }

    handleError(error: any) {
        //this.errorMessage = error;
        this.isLoading = false;
    }

}
