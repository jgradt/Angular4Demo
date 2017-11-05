import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms'

import { AuthenticationService } from '../../services/index'

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    mainForm: FormGroup;
    model: any = {};
    isLoading = false;
    returnUrl: string;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private fb: FormBuilder,
        private authenticationService: AuthenticationService) {

        this.createForm();
    }

    ngOnInit() {
        // reset login status
        this.authenticationService.logout();

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    createForm() {
        this.mainForm = this.fb.group({
            username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(12)]],
            password: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(12)]]
        });
    }

    login() {
        this.isLoading = true;
        let username = this.mainForm.get('username') ? (<FormControl>this.mainForm.get('username')).value : '';
        let password = this.mainForm.get('password') ? (<FormControl>this.mainForm.get('password')).value : '';
        this.authenticationService.login(username, password).subscribe(
            (result: boolean) => {
                this.router.navigate([this.returnUrl]);
            },
            (error: string) => {
                console.log(error);
                this.isLoading = false;
            });
    }

}
