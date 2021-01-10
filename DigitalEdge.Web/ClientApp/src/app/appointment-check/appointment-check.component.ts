import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators} from '@angular/forms';
import { AppointmentsService } from '../appointments/appointments.service';
import { AppointmentCheckService } from './appointment-check.service';


@Component({
  selector: 'app-appointment-check',
  templateUrl: './appointment-check.component.html',
})
export class AppointmentCheckComponent implements OnInit {
  appointmentcheckData: any = [];
  isSubmitted: boolean = false;
  appointmentcheckForm: FormGroup;

  constructor(private router: Router,
    private fb: FormBuilder,
    private _appointmentCheckService: AppointmentCheckService,
  ) { }
  ngOnInit() {
    this.appointmentcheckForm = this.fb.group({
      firstName: ["", Validators.required],     
    });
  }  
  get g() {
    return this.appointmentcheckForm.controls;
  }
  Submit() {
    this.isSubmitted = true;
    const model = this.appointmentcheckForm.value;
    if (this.appointmentcheckForm.invalid) {
      return;
    }
    this._appointmentCheckService.getAppointmentCheck(model).subscribe(
      res => {
        debugger;
        this.appointmentcheckData = res;
      });
  }
}

