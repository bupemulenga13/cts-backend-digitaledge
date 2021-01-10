import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { SendBuilSMSService } from '../send-bulk-sms/send-bulk-sms.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AddUserService } from '../adduser/adduser.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { FacilityManagmentService } from './building-managment.service';




@Component({
  selector: 'app-building-managment',
  templateUrl: './building-managment.component.html',
})
export class FacilityManagmentComponent implements OnInit {
  facilitys: any = [];
  districtList: any = [];
  provinceList: any = [];
  facilityscreate: any = [];
  facilityUserlist: any = [];
  facilitydistrictlist: any = [];
  facilityForm: FormGroup;
  checkValidation: string = "";
  errorMessage: any;
  facilityServicelist: any = [];
  Servicelist: any = [];
  confirmResult: any;
  facilityCreateForm: FormGroup;
  facilitydelete: FormGroup;
  servicepointdelete: FormGroup;
  provinceDefault = 0;
  districtDefault = 0;
  facilityDefault = 0;
  servicepointDefault = 0;
  emptyServicePointName = false;
  submitted = false;
  isSubmitted = false;
  currentFacility: any;
  currentservicepoint: any;
  buidlingid: any;
  deletesubmitted = false;
  deleteservicesubmitted = false;


  constructor(private router: Router,
    private _sendBuilSMSService: SendBuilSMSService,
    private fb: FormBuilder,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private _addUserService: AddUserService,
    private _facilityservice: FacilityManagmentService,
    private SpinnerService: NgxSpinnerService,
  ) { }
  ngOnInit() {
    this.submitted = false;
    this.isSubmitted = false;
    this.facilityForm = this.fb.group({
      servicePointId: 0,
      facilityId: [''],
      servicePointName: ['', Validators.required],
      facilityName: ['']
    });
    this.facilityCreateForm = this.fb.group({
      facilityId: 0,
      facilityName: ['', Validators.required],
      districtId: [0, Validators.required],
      provinceId: [0, Validators.required],
      facilityContactNumber: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]]
    });
    this.facilitydelete = this.fb.group({
      facilityId: 0,
      facilityName: ['', Validators.required],
      assignedfacilityId: 0,
    });
    this.servicepointdelete = this.fb.group({
      servicePointId: 0,
      servicePointName: ['', Validators.required],
      assignedservicePointId: 0,
    });
    this.facilityscreate = this.facilityCreateForm;
    const RoleName = localStorage.getItem('RoleName');
    if (RoleName === "Admin") {
      this.FacilityData();
      this.GetProvince();
    }
  }
  FacilityData() {
    this._sendBuilSMSService.GetFacility().subscribe(data => {
      if (data.length > 0) {
        this.facilitys = data;
      }
    });
  }
  CreateServicePoint(targetModal, user) {
    this.Servicelist = [];
    this.Getservicedata();
    this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static'
    });
    this.facilityForm.patchValue({
      facilityId: user.facilityId,
      facilityName: user.facilityName,
      servicePointName: user.servicePointName,
    });
  }
  get f() {
    return this.facilityForm.controls;
  }
  get g() {
    return this.facilityCreateForm.controls;
  }
  get d() {
    return this.facilitydelete.controls;
  }
  get service() {
    return this.servicepointdelete.controls;
  }
  onSubmit() {
    this.submitted = true;
    const model = this.facilityForm.value;
    if (this.facilityForm.invalid) {
      return;
    }
    this.CheckValidations();
    if (this.checkValidation == "") {
      if (this.facilityForm.value.servicePointId == 0) {
        this._addUserService.saveServicePoints(model)
          .subscribe((data) => {
            if (data.statusCode === 200) {
              this.Getservicedata();
              this.toastr.success('Service Point saved sucessfully', 'Success', {
                timeOut: 3000
              });
              this.clearServices();
            }
            else if (data.statusCode === 500) {
              this.toastr.error('Service Point Name Already Exist', 'Success', {
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
      else if (this.facilityForm.value.servicePointId > 0) {
        this._addUserService.editServicePoints(model)
          .subscribe((data) => {
            if (data.statusCode === 200) {
              this.Getservicedata();
              this.toastr.success('Service Point update sucessfully', 'Success', {
                timeOut: 3000
              });
            }
            else if (data.statusCode === 500) {
              this.toastr.error('Service Point Name Already Exist', 'Success', {
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
    }
    else {
      return;
    }
  }
  CheckValidations() {
    this.checkValidation = "";
    if (this.facilityForm.value.servicePointName == "" || this.facilityForm.value.servicePointName == undefined) {
      this.checkValidation = "Service Point Name";
      return this.checkValidation;
    }
    return this.checkValidation;
  }
  Getservicedata() {
    this.Servicelist = [];
    this._addUserService.getservicedata().subscribe(
      data => {
        if (data !== null) {
          this.facilityServicelist = data;
          if (this.currentservicepoint) {
            this.facilityServicelist.forEach(function (item, index) {
              if (item.facilityId === this.facilityForm.value.facilityId) {
                this.Servicelist.push(item);
                if (item.servicePointId === this.currentservicepoint) {
                  this.Servicelist.pop(item);
                }
              }
            }.bind(this));
          }
          else {
            this.facilityServicelist.forEach(function (item, index) {
              if (item.facilityId === this.facilityForm.value.facilityId) {
                this.Servicelist.push(item);
              }
            }.bind(this));
          }
        }
      },
    );
  }

  EditServicePoint(user) {
    this.Servicelist = [];
    this.Getservicedata();
    this.facilityForm.patchValue({
      servicePointName: user.servicePointName,
      facilityId: user.facilityId,
      facilityName: user.facilityName,
      servicePointId: user.servicePointId,
    });
  }
  DeleteServicePoint(targetModal, servicepointuser) {
    this.currentservicepoint = servicepointuser.servicePointId;
    this.Getservicedata();
    this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static'
    });
    this.servicepointDefault = servicepointuser.servicePointId;
  }
  DeleteServiceSubmited() {
    this.deleteservicesubmitted = true;
    this.servicepointdelete.controls.servicePointId.setValue(this.currentservicepoint);
    const model = this.servicepointdelete.value;
    if (this.servicepointdelete.invalid) {
      return;
    }
    this._facilityservice.deleteServicePoint(model)
      .subscribe((data) => {
        if (data.statusCode === 200) {
          this.toastr.success('Service Point  delete sucessfully', 'Success', {
            timeOut: 3000
          });
          this.closeservicepoint();
        }
        else {
          this.toastr.error('Oops, something went wrong', 'Failed', {
            timeOut: 3000
          });
        }
        this.SpinnerService.hide();
      }, error => this.errorMessage = error)
  } 


  DeletePopUp(targetModal, deleteuser) {
    this.currentFacility = deleteuser.facilityId;
    this.GetfacilityUserdata();
    this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static'
    });
      this.facilityDefault = deleteuser.facilityId;
  }

  DeleteSubmited() {
    this.deletesubmitted = true;
    this.facilitydelete.controls.facilityId.setValue(this.currentFacility);
    const model = this.facilitydelete.value;
    if (this.facilitydelete.invalid) {
      return;
    }
    this._facilityservice.deleteFacility(model)
      .subscribe((data) => {
        if (data.statusCode === 200) {
            this.toastr.success(data.message, 'Success', {
            timeOut: 3000
          });
          this.closedeletepopup();
        }        
        else {
          this.toastr.error(data.message, 'Failed', {
            timeOut: 3000
          });
        }
        this.SpinnerService.hide();
      }, error => this.errorMessage = error)
  } 

  close() {
    this.submitted = false;
    this.modalService.dismissAll();
    this.clearServices();
  }
  closedeletepopup() {
    this.deletesubmitted = false;
    this.facilityDefault = 0;
    this.currentFacility = "";
    this.modalService.dismissAll();
    this.facilitydelete.reset();
    this.facilitydelete = this.fb.group({
      facilityId: 0,
      facilityName: ['', Validators.required],
      assignedfacilityId: 0,
    });
    this.ngOnInit();
  }
  closeservicepoint() {
    this.deleteservicesubmitted = false;
    this.servicepointDefault = 0;
    this.currentservicepoint = "";
    this.modalService.dismissAll();
    this.servicepointdelete.reset();
    this.servicepointdelete = this.fb.group({
      servicePointId: 0,
      servicePointName: ['', Validators.required],
      assignedservicePointId: 0,
    });
  }
  CreateFacility(targetModal, facilityUser) {

    this.GetfacilityUserdata();
    this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static'
    });
    this.facilityCreateForm = this.fb.group({
      facilityId: 0,
      facilityName: ['', Validators.required],
      districtId: [0, Validators.required],
      provinceId: [0, Validators.required],
      facilityContactNumber: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]]
    });
    this.facilityCreateForm.patchValue({
      facilityId: 0,
      districtId: facilityUser.districtId,
      provinceId: facilityUser.provinceId,
    });
  }
  GetDistrictId(id) {    
    this.districtList = [];
    this._sendBuilSMSService.GetDistrict(id).subscribe(data => {
      if (data.length > 0) {
        this.districtList = data;
        this.provinceDefault = this.districtList[0].provinceId;
        this.districtDefault = 0;
      }
    });
  }
  GetProvince() {
    this.provinceList = [];
    this._sendBuilSMSService.GetProvince().subscribe(provinceData => {
      if (provinceData.length > 0) {
        this.provinceList = provinceData;
        this.districtDefault = 0;
      }
    });
  }
  Submit() {
    this.isSubmitted = true;
    const model = this.facilityCreateForm.value;
    if (this.facilityCreateForm.invalid) {
      return;
    }
    if (this.facilityCreateForm.value.facilityId == 0) {

      this._addUserService.savefacilityUser(model)
        .subscribe((data) => {
          if (data.statusCode === 200) {
            this.GetfacilityUserdata();
            this.isSubmitted = false;

            this.toastr.success('Saved sucessfully', 'Success', {
              timeOut: 3000
            });
            this.clearRecords();
            this.FacilityData();
          }
          else if (data.statusCode === 500) {
            this.toastr.error('Facility Name Already Exist', 'Failed', {
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
    else if (this.facilityCreateForm.value.facilityId > 0) {

      this._addUserService.editfacilityUser(model)
        .subscribe((data) => {
          if (data.statusCode === 200) {
            this.GetfacilityUserdata();
            this.isSubmitted = false;
            this.toastr.success('Update sucessfully', 'Success', {
              timeOut: 3000
            });
            this.clearRecords();
            this.FacilityData();
          }
          else if (data.statusCode === 500) {
            this.toastr.error('Facility Name Already Exist', 'Success', {
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
  }
  GetfacilityUserdata() {
    this._addUserService.getfacilityUserdata().subscribe(
      data => {
        if (data !== null) {
          this.facilityUserlist = data;
          if (this.currentFacility) {
            this.facilityUserlist.forEach(function (item, index) {
              if (item.facilityId === this.currentFacility) {
                this.facilityUserlist.splice(index, 1);
              }
            }.bind(this));
          }
          else {
            this.facilityUserlist = data;
          }
        }
      },
    );
  }
  EditProvince(user) {
    this.facilityCreateForm = this.fb.group({
      facilityId: 0,
      facilityName: ['', Validators.required],
      districtId: 0,
      districtName: ['', Validators.required,],
      provinceId: 0,
      provinceName: ['', Validators.required],
      facilityContactNumber: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]]
    });
    this.provinceDefault = 0;
    this.districtDefault = 0;
    this.provinceList = [];
    this.districtList = [];
    this.GetfacilityUserdata();
    this.GetProvince();
    this.provinceDefault = user.provinceId;
    this.districtDefault = Number(user.districtId);
    this.GetDistrictId(user.provinceId);
    this.facilityCreateForm.patchValue({
      districtId: user.districtId,
      districtName: user.districtName,
      facilityId: user.facilityId,
      facilityName: user.facilityName,
      provinceId: user.provinceId,
      provinceName: user.provinceName,
      facilityContactNumber: user.facilityContactNumber
    });
  }
  closepopup() {
    this.provinceDefault = 0;
    this.districtDefault = 0;
    this.isSubmitted = false;
    this.modalService.dismissAll();
    this.facilityCreateForm.reset();
  }
  clearRecords() {   
    this.facilityCreateForm.reset();  
    this.facilityCreateForm = this.fb.group({
      facilityId: 0,
      facilityName: ['', Validators.required],
      districtId: [0, Validators.required],
      provinceId: [0, Validators.required],
      facilityContactNumber: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]]
    });
    this.facilityCreateForm.patchValue({
      facilityId: 0,
      districtId: undefined,
      provinceId: undefined,
    });
    this.provinceDefault = 0;
    this.districtDefault = 0;
  }
  clearServices() {
    this.submitted = false;
    this.facilityForm.patchValue({
      servicePointId: 0,
      servicePointName: '',
    });
  }
  onchange(e) {
    this.districtDefault = e;
  }
  DeleteonChange(e) {
    this.facilitydelete.controls.assignedfacilityId.setValue(Number(e));
  }
  DeleteServiceonChange(e) {    
    this.servicepointdelete.controls.assignedservicePointId.setValue(Number(e));
  }
}
