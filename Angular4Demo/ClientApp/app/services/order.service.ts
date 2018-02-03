import { Injectable, Inject } from '@angular/core'
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/Observable";

import { PagedData, IOrder } from '../data-entities/index'
import { AppConfig } from '../infrastructure/index'

@Injectable()
export class OrderService {

    constructor(private http: HttpClient, private config: AppConfig) { }

    getPaged(pageIndex: number, pageSize: number): Observable<PagedData<IOrder>> {

        return this.http.get<PagedData<IOrder>>(this.config.apiBaseUrl + `orders?pageIndex=${pageIndex}&pageSize=${pageSize}`);

    }

    getByCustomerIdPaged(customerId: number, pageIndex: number, pageSize: number): Observable<PagedData<IOrder>> {

        return this.http.get<PagedData<IOrder>>(this.config.apiBaseUrl + `customers/${customerId}/orders?pageIndex=${pageIndex}&pageSize=${pageSize}`);

    }

    getById(id: number): Observable<IOrder> {

        return this.http.get<IOrder>(this.config.apiBaseUrl + 'orders/' + id);

    }

    create(order: IOrder): Observable<IOrder> {

        return this.http.post<IOrder>(this.config.apiBaseUrl + 'orders', order);

    }

    update(id: number, order: IOrder): Observable<Response> {

        return this.http.put<Response>(this.config.apiBaseUrl + 'orders/' + id, order);

    }

    delete(id: number): Observable<Response> {

        return this.http.delete<Response>(this.config.apiBaseUrl + 'orders/' + id);

    }
}