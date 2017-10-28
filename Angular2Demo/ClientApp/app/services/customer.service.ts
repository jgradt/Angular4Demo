﻿import { Injectable, Inject } from '@angular/core'
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/Observable";

import { ICustomer } from '../data-entities/customer'
import { PagedData } from '../data-entities/pagedData'

@Injectable()
export class CustomerService {

    constructor(private _http: HttpClient, @Inject('BASE_API_URL') private baseApiUrl: string) { }

    getPaged(pageIndex: number, pageSize: number): Observable<PagedData<ICustomer>> {

        return this._http.get<PagedData<ICustomer>>(this.baseApiUrl + `api/customers?pageIndex=${pageIndex}&pageSize=${pageSize}`);

    }

    getById(id: number): Observable<ICustomer> {

        return this._http.get<ICustomer>(this.baseApiUrl + 'api/customers/' + id);

    }
}