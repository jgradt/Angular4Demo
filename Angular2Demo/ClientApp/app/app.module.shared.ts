import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';

import { CustomerListComponent } from './components/customer/customer-list.component'
import { CustomerEditComponent } from './components/customer/customer-edit.component'
import { CustomerService } from './components/customer/customer.service'


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,

        CustomerListComponent,
        CustomerEditComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'customers', component: CustomerListComponent },
            { path: 'customers/:id', component: CustomerEditComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        CustomerService
    ]
})
export class AppModuleShared {
}
