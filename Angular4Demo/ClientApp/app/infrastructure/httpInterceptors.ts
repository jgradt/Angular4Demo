import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';

import { AppConfig } from './app.config'

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private config: AppConfig) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Get the auth header from the service.
        const token = localStorage.getItem(this.config.authTokenName);

        if (token) {
            // Clone the request to add the new header.
            const authReq = req.clone({ headers: req.headers.set('Authorization', `Bearer ${token}`) });
            // Pass on the cloned request instead of the original request.
            return next.handle(authReq);
        } else {
            return next.handle(req);
        }
    }
}

@Injectable()
export class TimingInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const started = Date.now();
        return next
            .handle(req)
            .do(event => {
                if (event instanceof HttpResponse) {
                    const elapsed = Date.now() - started;
                    console.log(`[${req.method}] Request for ${req.urlWithParams} took ${elapsed} ms.`);
                }
            });
    }
}