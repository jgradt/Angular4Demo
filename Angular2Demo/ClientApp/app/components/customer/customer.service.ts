import { Injectable, Inject } from '@angular/core'
import { Http, Response } from '@angular/http';
import { Observable } from "rxjs/Observable";

import { ICustomer } from './customer'

@Injectable()
export class CustomerService {

    constructor(private _http: Http, @Inject('BASE_URL') private _baseUrl: string) { }

    getAll(): Observable<Response> {

        return this._http.get(this._baseUrl + 'api/customers');

    }
}