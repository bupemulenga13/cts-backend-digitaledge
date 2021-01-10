import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from './dashboard.service';
import { AddUserService } from '../adduser/adduser.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { SimpleModalService } from 'ngx-simple-modal';
import { ConfirmComponent } from '../confirm/confirm.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SendBuilSMSService } from '../send-bulk-sms/send-bulk-sms.service';
import * as $ from "jquery";



  
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  loading = false;
  users: any = [];
  confirmResult = null;
  editProfileForm: FormGroup;
  title = 'modal2';
  FacilityList: any[];
  ServicePointList: any[];
  facilitylist: any[];
  errorMessage: any;
  checkValidation: string = "";
  isSubmitted = false;
 



  constructor(private router: Router,
    private _dashboardService: DashboardService,
    private _addUserService: AddUserService,
    private toastr: ToastrService,
    private SpinnerService: NgxSpinnerService,
    private _simpleModalService: SimpleModalService,
    private modalService: NgbModal,
    private fb: FormBuilder,
    private _sendBuilSMSService: SendBuilSMSService,


  ) { }

  ngOnInit() {
    this.Getfacilitydata();
    this.editProfileForm = this.fb.group({
      email: [''],
      facility: ['', Validators.required],
      userId: [''],
      servicepoint: ['', Validators.required]
    });
    const RoleName = localStorage.getItem('RoleName');
    if (RoleName === "Admin") {
      this.dashboardData();
    }
    else {
      this.router.navigate(['/']);
    }
  }
  dashboardData() {
    this._dashboardService.Getdetails().subscribe(
      data => {
        if (data !== null) {
          this.users = data;
          localStorage.setItem('users', JSON.stringify(this.users));
        }
        else {
          this.router.navigate(['']);
        }
      },
      error => {
      }
    );
  }
  deletepopup(deleteuser) {
    this._simpleModalService.addModal(ConfirmComponent, {
      title: 'Confirmation',
      message: 'Are you sure you want to do this? '
    })
      .subscribe((isConfirmed) => {
        this.confirmResult = isConfirmed;
        if (this.confirmResult === true) {
          this.deleteUser(deleteuser);
        }
      });
  }
  deleteUser(user) {
    user.isDeleted = true;
    this.SpinnerService.show();
    this._addUserService.deleteUser(user).subscribe((data) => {
      if (data.statusCode === 200) {
        this.toastr.success('User deleted sucessfully', 'Success', {
          timeOut: 3000
        });

        this.users.forEach(function (item, index) {
          if (item.id === user.id) {
            this.users.splice(index, 1);
          }
        }.bind(this));
      }
      else {
        this.toastr.error('Oops, something went wrong', 'Failed', {
          timeOut: 3000
        });
      }
      this.SpinnerService.hide();
    }, error => console.error(error))
  }

  facilitypopup(targetModal, user) {
    this.Getfacilitydata();
    this._sendBuilSMSService.GetFacility().subscribe(data => {
      if (data.length > 0) {
        this.FacilityList = data;
      }
    });
    this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static'
    });
    this.editProfileForm.patchValue({
      userId: user.id,
      email: user.email
    });
  }
  onSubmit() {
    this.isSubmitted = true;
    if (this.editProfileForm.invalid) {
      return;
    }
    const model = this.editProfileForm.value;
    if (this.checkValidation == "") {
      this._addUserService.savefacilityUser(model)
        .subscribe((data) => {
          if (data.statusCode === 200) {
            this.Getfacilitydata();
            this.clearRecords();
            this.toastr.success('User saved sucessfully', 'Success', {
              timeOut: 3000
            });
          }
          else if (data.statusCode === 500) {
            this.toastr.error('User Already Exist', 'Success', {
              timeOut: 3000
            });
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
      this.toastr.error('Please Fill Facility And Service Point Name', '', {
        timeOut: 2000
      });
    }

  }
  GetServicePointId(value) {
    if (value != undefined && value != "") {
      var index = this.FacilityList.findIndex(x => x.facilityId == Number(value));
      this.editProfileForm.value.facility = this.FacilityList[index].facilityId;
      this.GetServicePoint(Number(value));
    }
  }
  GetServicePointName(value) {
    var index = this.ServicePointList.findIndex(x => x.servicePointId == Number(value));
    this.editProfileForm.value.servicepoint = this.ServicePointList[index].servicePointId;
  }
  GetServicePoint(id) {
    this.ServicePointList = [];
    this._sendBuilSMSService.GetServicePoint(id).subscribe(servicePointData => {
      if (servicePointData.length > 0) {
        this.ServicePointList = servicePointData;
      }
    });
  }
  get g() {
    return this.editProfileForm.controls;
  }
  Getfacilitydata() {
    this._addUserService.getfacilitydata().subscribe(
      data => {
        if (data !== null) {
          this.facilitylist = data;
        }
      },
      error => {
      }
    );
  }
  deleteFacilitypopup(deleteuser) {
    $(".d-block").attr("style", "display:none!important");
    $('.modal-backdrop.show').css('opacity', '0');
    $('.modal-backdrop.show').css('z-index', '0');

    this._simpleModalService.addModal(ConfirmComponent, {
      title: 'Confirmation',
      message: 'Are you sure you want to do this? '
    })
      .subscribe((isConfirmed) => {
        this.confirmResult = isConfirmed;
        if (this.confirmResult === true) {
          this.deletefacilityUser(deleteuser);
        }
        $(".d-block").attr("style", "display:block!important");
        $('.modal-backdrop.show').css('opacity', '.5');
        $('.modal-backdrop.show').css('z-index', '1050px!important');
      });
  }
  deletefacilityUser(user) {
    user.isActive = true;
    this.SpinnerService.show();
    this._addUserService.deleteFacilityUser(user).subscribe((data) => {
      if (data.statusCode === 200) {
        this.Getfacilitydata();
        this.toastr.success('User deleted sucessfully', 'Success', {
          timeOut: 3000
        });
        this.users.forEach(function (item, index) {
          if (item.id === user.id) {
            this.users.splice(index, 1);
          }
        }.bind(this));
      }
      else {
        this.toastr.error('Oops, something went wrong', 'Failed', {
          timeOut: 3000
        });
      }
      this.SpinnerService.hide();
    }, error => console.error(error))
  }

  close() {
    this.isSubmitted = false;
    this.modalService.dismissAll();
    this.editProfileForm.patchValue({
      facility: '',
      servicepoint: '',
    });
  }
  clearRecords() {   
    this.isSubmitted = false;
    this.editProfileForm.patchValue({
      facility: '',
      servicepoint: '',
    });
  }
}

 

