import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { ActivatedRoute, Router } from "@angular/router";

import { ICustomer } from '../../data-entities/index'
import { CustomerService } from "../../services/index";

@Component({
    templateUrl: './customer-edit.component.html'
})
export class CustomerEditComponent implements OnInit {

    pageTitle = 'Edit Customer';
    mainForm: FormGroup;
    customer: ICustomer;
    isLoading: boolean;
    isEdit: boolean = true;
    errorMessage: string;

    constructor(private fb: FormBuilder,
        private customerService: CustomerService,
        private route: ActivatedRoute,
        private router: Router) {

    }

    ngOnInit(): void {

        // initialize form
        this.mainForm = this.fb.group({
            firstName: ['', [Validators.required, Validators.maxLength(50)]],
            lastName: ['', [Validators.required, Validators.maxLength(50)]],
            email: ['', [Validators.email, Validators.maxLength(100)]]
        });

        // load data
        let id = +(this.route.snapshot.paramMap.get('id') || 0);
        this.loadCustomer(id);
    }

    loadCustomer(id: number) : void {

        if (id === 0) {
            // new data
            let customer: ICustomer = {
                id: 0,
                firstName: '',
                lastName: '',
                email: ''
            };

            this.onDataRetrieved(customer);
        } else {
            // existing data (edit)
            this.isLoading = true;
            this.customerService.getById(id).subscribe(result =>
                this.onDataRetrieved(result),
                error => this.handleError(error));
        }
    }

    onDataRetrieved(customer: ICustomer): void {
        if (this.mainForm) {
            this.mainForm.reset();
        }
        this.customer = customer;

        if (this.customer.id === 0) {
            this.pageTitle = 'Add Customer';
            this.isEdit = false;
        } else {
            this.pageTitle = 'Edit Customer';
            this.isEdit = true;
        }

        // Update the data on the form
        this.mainForm.patchValue({
            firstName: this.customer.firstName,
            lastName: this.customer.lastName,
            email: this.customer.email
        });

        this.isLoading = false;
    }

    handleError(error: any) {
        this.errorMessage = error;
        this.isLoading = false;
    }

    submitForm(): void {
        if (this.mainForm.dirty && this.mainForm.valid) {

            let o = Object.assign({}, this.customer, this.mainForm.value);

            if (this.isEdit) {
                this.customerService.update(o.id, o).subscribe(
                    () => this.onSaveComplete(),
                    (error: any) => this.errorMessage = <any>error
                    );
            } else {
                this.customerService.create(o).subscribe(
                    () => this.onSaveComplete(),
                    (error: any) => this.errorMessage = <any>error
                    );
            }
            
        } else if (!this.mainForm.dirty) {
            this.onSaveComplete();
        }
    }

    onSaveComplete(): void {
        // Reset the form to clear the flags
        this.mainForm.reset();
        this.router.navigate(['/customers']);
    }

}