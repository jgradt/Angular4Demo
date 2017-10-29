// angular imports
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { PaginationModule } from 'ngx-bootstrap';

// app imports
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CustomerListComponent } from './components/customer/customer-list.component'
import { CustomerEditComponent } from './components/customer/customer-edit.component'
import { CustomerService } from './services/customer.service'


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CustomerListComponent,
        CustomerEditComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'customers', component: CustomerListComponent },
            { path: 'customers/:id', component: CustomerEditComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        PaginationModule.forRoot()
    ],
    providers: [
        CustomerService,
        { provide: 'BASE_API_URL', useValue: 'http://localhost:54618/' }
    ]
})
export class AppModuleShared {
}
