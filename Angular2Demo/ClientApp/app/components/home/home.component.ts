import { Component } from '@angular/core';

import { AuthenticationService } from '../../services/index'

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    constructor(private authService: AuthenticationService) { }

    logout(): void {
        this.authService.logout();
        alert('logout successful');
    }
}
