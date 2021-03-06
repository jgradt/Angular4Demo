﻿import { Component, OnInit } from '@angular/core';

import { PagedData, ICustomer } from '../../data-entities/index'
import { CustomerService } from "../../services/index";

@Component({
    templateUrl: './customer-list.component.html',
    styleUrls: ['../../styles.css']
})
export class CustomerListComponent implements OnInit {

    data: PagedData<ICustomer>;
    public totalPages: number = 0;
    currentPage: number = 1;
    isLoading: boolean;

    constructor(private _customerService: CustomerService) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.loadData();
    }

    loadData(): void {
        this._customerService.getPaged(this.currentPage - 1, 10).subscribe(result => {
            this.data = result;
            this.currentPage = this.data.pageIndex + 1;
            this.isLoading = false;
        }, error => this.handleError(error));
    }

    pageChanged(event: any): void {

        this.currentPage = event.page;
        this.loadData();
    }

    handleError(error: any) {
        //this.errorMessage = error;
        this.isLoading = false;
    }

}