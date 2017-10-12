import { Injectable, Inject } from '@angular/core'
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/Observable";

import { ICustomer } from './customer'

@Injectable()
export class CustomerService {

    constructor(private _http: HttpClient, @Inject('BASE_URL') private _baseUrl: string) { }

    getAll(): Observable<ICustomer[]> {

        return this._http.get<ICustomer[]>(this._baseUrl + 'api/customers');

    }

    getById(id: number): Observable<ICustomer> {

        return this._http.get<ICustomer>(this._baseUrl + 'api/customers/' + id);

    }
}