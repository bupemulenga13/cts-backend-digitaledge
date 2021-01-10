import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { LoginService } from './login.service';



@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent {
    model: any = {};
    emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+[a-z]{2,4}$";
    RoleName: string;
    Name: string;

    loading = false;
    errorMessage: string;
    constructor(
        private router: Router,
        private _loginService: LoginService,
        private toastr: ToastrService,
        private SpinnerService: NgxSpinnerService
    ) { }

    ngOnInit() {
        this.SpinnerService.hide();
    }
    login() {
        this.SpinnerService.show();
        this._loginService.Login(this.model).subscribe(
            data => {
                if (data.data !== null) {
                    const jwt = data.token;
                    if (jwt !== "") {
                        const jwtData = jwt.split('.')[1]
                        const decodedJwtJsonData = window.atob(jwtData)
                        const decodedJwtData = JSON.parse(decodedJwtJsonData)
                        this.RoleName = decodedJwtData.role;
                        this.Name = decodedJwtData.unique_name;
                        localStorage.setItem('Name', decodedJwtData.unique_name);
                        localStorage.setItem('RoleName', this.RoleName);
                    }
                    localStorage.setItem('Token', data.token);
                    if (localStorage.getItem('Token')) {
                        localStorage.setItem('Email', JSON.stringify(data.user.email));
                        this.toastr.success('Login Sucessfully', 'Success', {
                            timeOut: 3000
                        });
                        if (this.RoleName === "Admin") {
                            this.router.navigate(['/users']);
                        }
                        else {
                            this.router.navigate(['/appointments']);
                        }
                        this.SpinnerService.hide();
                    }
                    else {
                        this.SpinnerService.hide();
                        this.router.navigate(['']);
                        this.toastr.error('Bad Request', 'Failed', {
                            timeOut: 3000
                        });
                    }
                }
                else {
                    this.SpinnerService.hide();
                    this.router.navigate(['']);
                    if (data.statusCode === 401) {
                        this.toastr.error('The username or password you entered is incorrect!', 'Failed', {
                            timeOut: 3000
                        });
                    }
                    else {
                        this.toastr.error('Bad Request', 'Failed', {
                            timeOut: 3000
                        });
                    }
                }
            },
            error => {
            });
    };
} 
