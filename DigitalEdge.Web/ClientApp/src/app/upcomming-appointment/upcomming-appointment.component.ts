import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddUserService } from '../adduser/adduser.service';
import { AppointmentsService } from '../appointments/appointments.service';
import * as Chart from 'chart.js';
import { UpcommingAppointmentService } from './upcomming-appointment.service';

@Component({
  selector: 'app-upcomming-appointment',
  templateUrl: './upcomming-appointment.component.html',
})
export class UpcommingAppointmentComponent implements OnInit {
  provinceList: any = [];
  Servicelist: any = []; 
  submitted: boolean = false;
  facilityForm: FormGroup;
  facilitys: any = [];
  facilityServicelist: any = [];
  districtList: any = [];
  myChart: any = [];
  temp_age: any = [];
  alldates: any = [];
  apppointmentDates: any = [];
  appointmentsdata: any = [];

  public upcommingData: UpcommingData = new UpcommingData();

  constructor(private router: Router,
    private fb: FormBuilder,
    private _addUserService: AddUserService,
    private _appointmentsService: AppointmentsService,
    private _upcommingAppointmentService: UpcommingAppointmentService,
  ) { }
  ngOnInit() {
    this.facilityForm = this.fb.group({
      servicePointId: [""],   
    });
    this.GetServicePoint();
    this.Appointments();
  }
   GetServicePoint() {
    this.Servicelist = [];
    this._addUserService.getservicedata().subscribe(
      data => {
        if (data !== null) {
          this.Servicelist=data;        
        }
      },
    );
  }

  Appointments() {
    this._appointmentsService.getAppointmentDetails().subscribe(
      res => {
        this.appointmentsdata = res;
        this.appointmentsdata.forEach(function (item, index) {
          this.temp_age.push(item.age);
          this.alldates.push(item.appointmentDate);
        }.bind(this));
        this.alldates.forEach((date) => {
          let jsdate = new Date(date);
          const options = { year: 'numeric', month: 'long', day: 'numeric' } as const;
          this.apppointmentDates.push(jsdate.toLocaleDateString(undefined, options));
        })
        this.myChart = new Chart('canvas', {
          type: 'line',
          data: {
            labels: this.apppointmentDates,
            datasets: [
              {
                label: "Age",
                fill: false,
                backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
                borderColor: 'black',
                data: this.temp_age,
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
    if (event.currentTarget.classList[1] === "upcommingap") {
      var x = document.getElementById("upcommingapp");
      if (x.style.display === "none") {
        return x.style.display = "block";
      }
    }
    return x.style.display = "none";
  }
  get f() {
    return this.facilityForm.controls;
  }
  Submit() {
    this.submitted = true;  
    this.upcommingData.ServicePointId = Number(this.facilityForm.value.servicePointId);
    this._upcommingAppointmentService.getUpcommingAppointmentFilter(this.upcommingData).subscribe(
      res => {
        this.appointmentsdata = [];
        this.temp_age = [];
        this.alldates = [];
        this.apppointmentDates = [];
        $('#chartType option:contains("Line")').prop('selected', true);
        $("#chartType").val("line");
        this.appointmentsdata = res;
        this.appointmentsdata.forEach(function (item, index) {
          this.temp_age.push(item.age);
          this.alldates.push(item.appointmentDate);
        }.bind(this));
        this.alldates.forEach((date) => {
          let jsdate = new Date(date);
          const options = { year: 'numeric', month: 'long', day: 'numeric' } as const;
          this.apppointmentDates.push(jsdate.toLocaleDateString(undefined, options));
        })
        this.myChart.destroy();
        this.myChart = new Chart('canvas', {
          type: 'line',
          data: {
            labels: this.apppointmentDates,
            datasets: [
              {
                label: "Age",
                fill: false,
                backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
                borderColor: 'black',
                data: this.temp_age,
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

  updateUpcommingAppChartType(event) {
    this.myChart.destroy();
    this.myChart = new Chart('canvas', {
      type: event,
      data: {
        labels: this.apppointmentDates,
        datasets: [
          {
            label: "Age",
            fill: false,
            backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
            borderColor: 'black',
            data: this.temp_age,
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
export class UpcommingData {
  ServicePointId: any = 0;
}
