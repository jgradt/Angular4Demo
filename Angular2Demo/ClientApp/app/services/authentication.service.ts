import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/do'

import { AppConfig } from '../infrastructure/app.config';

@Injectable()
export class AuthenticationService {
    constructor(private http: HttpClient, private config: AppConfig) { }

    login(username: string, password: string): Observable<boolean> {
        return this.http.post<AuthToken>(this.config.apiBaseUrl + 'token', { userName: username, password: password })
            .do(result => {
                localStorage.setItem('currentToken', result.token);
            })
            .map(result => true);
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentToken');
    }
}

export interface AuthToken {
    token: string;
}
