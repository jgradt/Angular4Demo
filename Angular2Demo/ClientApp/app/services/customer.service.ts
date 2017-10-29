import { Injectable, Inject } from '@angular/core'
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

    create(customer: ICustomer): Observable<ICustomer> {

        return this._http.post<ICustomer>(this.baseApiUrl + 'api/customers', customer);

    }

    update(id: number, customer: ICustomer): Observable<Response> {

        return this._http.put<Response>(this.baseApiUrl + 'api/customers/' + id, customer);

    }

    delete(id: number): Observable<Response> {

        return this._http.delete<Response>(this.baseApiUrl + 'api/customers/' + id);

    }
}