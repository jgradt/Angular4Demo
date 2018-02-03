// angular imports
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { PaginationModule } from 'ngx-bootstrap';

// app imports
import { AppComponent } from './components/app/app.component';

import { TimingInterceptor, AuthInterceptor, AuthGuard, AppConfig } from './infrastructure/index';
import { CustomerService, AuthenticationService, OrderService } from './services/index';
import { ValidationMessageComponent } from './components/shared/index'

import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CustomerListComponent } from './components/customer/customer-list.component'
import { CustomerEditComponent } from './components/customer/customer-edit.component'
import { LoginComponent } from './components/login/login.component';
import { CustomerOrderListComponent } from './components/order/customer-order-list.component';
import { CustomerDetailsComponent } from './components/customer/customer-details.component'

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CustomerListComponent,
        CustomerEditComponent,
        LoginComponent,
        ValidationMessageComponent,
        CustomerOrderListComponent,
        CustomerDetailsComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'login', component: LoginComponent },
            { path: 'home', component: HomeComponent },
            { path: 'customers', component: CustomerListComponent, canActivate: [AuthGuard] },
            { path: 'customers/add', component: CustomerEditComponent, canActivate: [AuthGuard] },
            { path: 'customers/:id/edit', component: CustomerEditComponent, canActivate: [AuthGuard] },
            { path: 'customers/:id/details', component: CustomerDetailsComponent, canActivate: [AuthGuard] },
            { path: '**', redirectTo: 'home' }
        ]),
        PaginationModule.forRoot()
    ],
    providers: [
        CustomerService,
        OrderService,
        AuthenticationService,
        AppConfig,
        AuthGuard,
        { provide: HTTP_INTERCEPTORS, useClass: TimingInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
    ]
})
export class AppModuleShared {
}
