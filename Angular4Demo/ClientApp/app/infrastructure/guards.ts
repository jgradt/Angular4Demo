import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AppConfig } from './app.config'

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private config: AppConfig) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let returnVal = false;
        let authToken = localStorage.getItem(this.config.authTokenName);

        if (authToken) {
            let jwt = this.parseJwt(authToken);

            let now = Math.trunc(Date.now() / 1000);
            if (jwt.exp > now) {
                returnVal = true;
            } else {
                console.log('jwt token is expired');
            }
        }

        if (returnVal) {
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        return false;
    }

    parseJwt(token: string) {
        var base64Url = token.split('.')[1];
        var base64 = base64Url.replace('-', '+').replace('_', '/');
        return JSON.parse(window.atob(base64));
    }
}