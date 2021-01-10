import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AddUserService } from './adduser.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  templateUrl: './adduser.component.html'
})

export class AddUserComponent implements OnInit {
  userForm: FormGroup;
  title: string = "Add user";
  id: number;
  errorMessage: any;
  users: any = [];
  roleList: Array<any> = [];
 isSubmitted = false;


  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _userService: AddUserService,
    private _router: Router,
    private SpinnerService: NgxSpinnerService,
    private toastr: ToastrService,

) {
    if (this._avRoute.snapshot.params["id"]) {
      this.id = this._avRoute.snapshot.params["id"];
    }
    this.userForm = this._fb.group({
      id: 0,
      firstName: ['', [Validators.required]],
      lastName: ['', ''],
      password: ['', [Validators.required, Validators.minLength(4)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNo: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
      gender: ['male', ''],
      roleId: ['', Validators.required],
      roleName: ['', ''],
      isSuperAdmin: ['', ''],
      isDeleted: [false, ''],
    })
  }

  ngOnInit() {
    this._userService.getRoleList().subscribe(
      data =>
        this.roleList = data
       ) 
    $('.existsclass').hide();
    this.SpinnerService.hide();
    if (this.id > 0) {
      this.title = "Edit user";

      this._userService.getUserById(this.id)
        .subscribe((data) => {
          $('#email').prop('disabled', true);
          this.userForm.setValue(data)
        }, error => this.errorMessage = error);
    }
    else {     
      this.userForm.setValue(this.userForm.value);
    }
  }
  checkemail(e, email) {
    var emailexits = [];
    if (email.value != "") {
      this.users = JSON.parse(localStorage.getItem('users'));
      this.users.forEach(function (item, index) {
        if (item.email === email.value) {
          emailexits.push(email.value);
        }
      });
      if (emailexits.length > 0) {
        $('.existsclass').show();
        $('.btnSubmit').prop('disabled', true);
      }
      else {
        $('.existsclass').hide();      
        $('.btnSubmit').prop('disabled', false);
      }
        }
    else {
      $('.existsclass').hide();
      $('.btnSubmit').prop('disabled', false);
    }
  } 
  save() {
    this.isSubmitted = true;
    $('#email').prop('disabled', false);
    if (!this.userForm.valid) {
      return;
    }
    this.SpinnerService.show();
    if (this.userForm.value.id !== 0) {

      this._userService.updateUser(this.userForm.value)
        .subscribe((data) => {
         
          if (data.statusCode===200) {
            this.toastr.success('User update sucessfully', 'Success', {
              timeOut: 3000
            });
            this._router.navigate(['/users']);
          }
          else {
            this.toastr.error('Oops, something went wrong', 'Failed', {
              timeOut: 3000
            });
          }
          this.SpinnerService.hide();
        }, error => this.errorMessage = error)
    }
    else {
      this._userService.saveUser(this.userForm.value)
        .subscribe((data) => {
          if (data.statusCode === 200) {
            this.toastr.success('User saved sucessfully', 'Success', {
              timeOut: 3000
            });
            this._router.navigate(['/users']);
          }
          else {
            this.toastr.error('Oops, something went wrong', 'Failed', {
              timeOut: 3000
            });
          }
          this.SpinnerService.hide();

        }, error => this.errorMessage = error)
    }
  }
  cancel() {
    this._router.navigate(['/users']);
  }
  get firstName() { return this.userForm.get('firstName'); }
  get lastName() { return this.userForm.get('lastName'); }
  get email() { return this.userForm.get('email'); }
  get password() { return this.userForm.get('password'); }
  get gender() { return this.userForm.get('gender'); }
  get phoneNo() { return this.userForm.get('phoneNo');  }
  get roleId() { return this.userForm.get('roleId'); }
  get isSuperAdmin() { return this.userForm.get('isSuperAdmin'); }
  get isDeleted() { return this.userForm.get('isDeleted'); }
}
