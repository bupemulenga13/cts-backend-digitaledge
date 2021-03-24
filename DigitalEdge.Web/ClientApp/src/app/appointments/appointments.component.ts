import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AppointmentsService } from './appointments.service';
import { CommonServices } from '../Services/common-services';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AddUserService } from '../adduser/adduser.service';
import { SendBuilSMSService } from '../send-bulk-sms/send-bulk-sms.service';

@Component({
    selector: 'app-appointments',
    templateUrl: './appointments.component.html',
})
export class AppointmentsComponent implements OnInit {
  Isshow: boolean = false;
  Isappoinment: boolean = true
  appointmentsdata: any = [];
  confirmResult = null;
  viewdatadetails: any = [];
  appointmentCreateForm: FormGroup;
  facilityUserlist: any = [];
  Servicelist: any = [];
  facilityServicelist: any = [];
  currentservicepoint: any = [];
  appointmentCreate: any = [];
  facilitys: any = [];
  isSubmitted: boolean = false;
  errorMessage: any;
  minDate: { year: number, month: number, day: number };

    constructor(private router: Router,
      private _appointmentsService: AppointmentsService,
      private _sendBuilSMSService: SendBuilSMSService,
      private fb: FormBuilder,
      private toastr: ToastrService,
      private SpinnerService: NgxSpinnerService,
      private modalService: NgbModal,
      private _addUserService: AddUserService,
      private _commonService: CommonServices) {
        this._commonService.appoinementOninit.subscribe(value => {
            this.Appointments();
        })
    }
  ngOnInit() {      
    this.appointmentCreateForm = this.fb.group({
      facilityId: ['', Validators.required],
      servicePointId: ['', Validators.required],
      clientId: ['', ''],
      appointmentDate: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNo: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]]
    });
    this.appointmentCreate = this.appointmentCreateForm;
      this.Appointments();
    }
    Appointments() {
        this.Isshow = false;
        this.Isappoinment = true;
        this.SpinnerService.show();
        this._appointmentsService.getAppointmentDetails().subscribe(
            data => {
                    this.appointmentsdata = data;
                    this.SpinnerService.hide();
            }
        );
    }
    SendReminder(appointment) {
        this._appointmentsService.SendReminder(appointment).subscribe(
            data => {
                if (data.message == 'Messages sent') {
                    this.toastr.success(data.message +' Successfully', '', {
                        timeOut: 3000
                    });
                    this.SpinnerService.hide();
                }
                else {
                    this.toastr.error(data.message, '', {
                        timeOut: 3000
                    });
                    this.SpinnerService.hide();
                }
            },
            error => {
            }
        );
    }
    ViewDetails(appointmentId) {
        this.Isshow = true;
        this.Isappoinment = false;
        this._appointmentsService.viewDetails(appointmentId).subscribe(
            data => {
                this.viewdatadetails = data;
            }
        );
  }

  CreateAppointment(targetModal, appointmentUser) {

    const current = new Date();
    this.minDate = {
      year: current.getFullYear(),
      month: current.getMonth() + 1,
      day: current.getDate()
    }; 
    this.FacilityData();
    this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static'
    });   
    this.appointmentCreateForm.patchValue({
      facilityId: "",
      servicePointId:"",
      clientId:"",
      firstName: appointmentUser.firstName,
      lastName: appointmentUser.lastName,
      appointmentDate: appointmentUser.appointmentDate,
      phoneNo: appointmentUser.phoneNo,
    });
  }
  FacilityData() {
    this._sendBuilSMSService.GetFacility().subscribe(data => {
      if (data.length > 0) {
        this.facilitys = data;
      }
    });
  }
  GetServicePoint(e) {
    this.Servicelist = [];
    this._addUserService.getservicedata().subscribe(
      data => {
        if (data !== null && data !== "") {
          this.facilityServicelist = data;
          this.facilityServicelist.forEach(function (item, index) {
            if (item.facilityId === Number(this.appointmentCreateForm.value.facilityId)) {
              this.Servicelist.push(item);
            }
          }.bind(this));
          this.appointmentCreateForm.get('facilityId').setValue(e.target.value, {
            onlySelf: true
          })
          this.appointmentCreateForm.get('servicePointId').setValue("", {
            onlySelf: true
          })
        }
      },
    );
  }
  get g() {
    return this.appointmentCreateForm.controls;
  }
  Submit() {
    this.isSubmitted = true;
    const model = this.appointmentCreateForm.value;
    if (this.appointmentCreateForm.invalid) {
      return;
    }
    this._appointmentsService.saveAppointmentUser(model)
        .subscribe((data) => {
          if (data.statusCode === 200) {
            this.isSubmitted = false;
            this.toastr.success('Saved sucessfully', 'Success', {
              timeOut: 3000
            });
            this.Appointments();
            this.clearRecords();
          }         
          else if(data.statusCode===500){
            this.toastr.error('Appointment already exit', 'Failed', {
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
  clearRecords() {
    this.isSubmitted = false;
    this.modalService.dismissAll();
    this.appointmentCreateForm.reset();
  }
  closepopup() {   
    this.isSubmitted = false;
    this.modalService.dismissAll();
    this.appointmentCreateForm.reset();
  }
}



