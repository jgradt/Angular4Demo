import { Injectable, Inject } from '@angular/core'
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/Observable";

import { ICustomer } from '../data-entities/customer'
import { PagedData } from '../data-entities/pagedData'
import { AppConfig } from '../infrastructure/app.config'

@Injectable()
export class CustomerService {

    constructor(private http: HttpClient, private config: AppConfig) { }

    getPaged(pageIndex: number, pageSize: number): Observable<PagedData<ICustomer>> {

        return this.http.get<PagedData<ICustomer>>(this.config.apiBaseUrl + `customers?pageIndex=${pageIndex}&pageSize=${pageSize}`);

    }

    getById(id: number): Observable<ICustomer> {

        return this.http.get<ICustomer>(this.config.apiBaseUrl + 'customers/' + id);

    }

    create(customer: ICustomer): Observable<ICustomer> {

        return this.http.post<ICustomer>(this.config.apiBaseUrl + 'customers', customer);

    }

    update(id: number, customer: ICustomer): Observable<Response> {

        return this.http.put<Response>(this.config.apiBaseUrl + 'customers/' + id, customer);

    }

    delete(id: number): Observable<Response> {

        return this.http.delete<Response>(this.config.apiBaseUrl + 'customers/' + id);

    }
}