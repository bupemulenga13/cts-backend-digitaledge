import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddUserService } from '../adduser/adduser.service';
import { AppointmentsService } from '../appointments/appointments.service';
import * as Chart from 'chart.js';
import { MissedAppointmentService } from './missed-appointment.service';

@Component({
  selector: 'app-missed-appointment',
  templateUrl: './missed-appointment.component.html',
})
export class MissedAppointmentComponent implements OnInit {
  Servicelist: any = [];
  submitted: boolean = false;
  facilityForm: FormGroup;
   myChart: any = [];
  age: any = [];
  allDates: any = [];
  missedAppointmentData: any = [];
  missedDates: any = [];
  public missedData: MissedAppointmentData = new MissedAppointmentData();

  constructor(private router: Router,
    private fb: FormBuilder,
    private _addUserService: AddUserService,
    private _appointmentsService: AppointmentsService,
    private _missedAppointmentService: MissedAppointmentService,
  ) { }
  ngOnInit() {
    this.facilityForm = this.fb.group({
      servicePointId: [""],
    });
    this.GetServicePoint();
    this.MissedAppointments();
  }
  GetServicePoint() {
    this.Servicelist = [];
    this._addUserService.getservicedata().subscribe(
      data => {
        if (data !== null) {
          this.Servicelist = data;          
        }
      },
    );
  }

  MissedAppointments() {
    this._appointmentsService.getAppointmentDetailsMissed().subscribe(
      data => {              
        this.missedAppointmentData = data;
        this.missedAppointmentData.forEach(function (item, index) {
          this.age.push(item.age);
          this.allDates.push(item.appointmentDate);
        }.bind(this));
        this.allDates.forEach((date) => {
          let jsdate = new Date(date);
          const options = {year: 'numeric', month: 'long', day: 'numeric' };
          this.missedDates.push(jsdate.toLocaleDateString(undefined, options));
        })
        this.myChart = new Chart('canvas', {
          type: 'line',
          data: {
            labels: this.missedDates,
            datasets: [
              {
                label: "Age",
                fill: false,
                backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
                borderColor: 'black',
                data: this.age,
              }
            ]
          },
          options: {
            legend: {
              display: false
            },
            scales: {
              xAxes: [{
                display: true
              }],
              yAxes: [{
                display: true
              }],
            }
          }
        });
      }
    );
  }


  show(event) {
    if (event.currentTarget.classList[1] === "missedap") {
      var x = document.getElementById("missedapp");
      if (x.style.display === "none") {
        return x.style.display = "block";
      }
    }
    return x.style.display = "none";
  }
 
  Submit() {
    this.submitted = true;
   
    this.missedData.ServicePointId = Number(this.facilityForm.value.servicePointId);
    this._missedAppointmentService.getAppointmentsMissedFilter(this.missedData).subscribe(
      res => {
        this.missedAppointmentData = [];
        this.age = [];
        this.allDates = [];
        this.missedDates = [];
        $('#chartType option:contains("Line")').prop('selected', true);
        $("#chartType").val("line");  
        this.missedAppointmentData = res;
        this.missedAppointmentData.forEach(function (item, index) {
          this.age.push(item.age);
          this.allDates.push(item.appointmentDate);
        }.bind(this));
        this.allDates.forEach((date) => {
          let jsdate = new Date(date);
          const options = { year: 'numeric', month: 'long', day: 'numeric' };
          this.missedDates.push(jsdate.toLocaleDateString(undefined, options));
        })
        this.myChart.destroy();
        this.myChart = new Chart('canvas', {
          type: 'line',
          data: {
            labels: this.missedDates,
            datasets: [
              {
                label: "Age",
                fill: false,
                backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
                borderColor: 'black',
                data: this.age,
              }
            ]
          },
          options: {
            legend: {
              display: false
            },
            scales: {
              xAxes: [{
                display: true
              }],
              yAxes: [{
                display: true
              }],
            }
          }
        });
      }
    );
  }
  updateMissed(event) {
    this.myChart.destroy();
    this.myChart = new Chart('canvas', {
      type: event,
      data: {
        labels: this.missedDates,
        datasets: [
          {
            label: "Age!",
            fill: false,
            backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
            borderColor: 'black',
            data: this.age,
          }
        ]
      },
      options: {
        legend: {
          display: false
        },
        scales: {
          xAxes: [{
            display: true
          }],
          yAxes: [{
            display: true
          }],
        }
      }
    });
  }
 
}
export class MissedAppointmentData {
  ServicePointId: any = 0;
}
