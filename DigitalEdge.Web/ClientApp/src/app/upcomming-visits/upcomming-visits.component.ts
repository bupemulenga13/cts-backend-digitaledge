import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { SendBuilSMSService } from '../send-bulk-sms/send-bulk-sms.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddUserService } from '../adduser/adduser.service';
import { AppointmentsService } from '../appointments/appointments.service';
import * as Chart from 'chart.js';
import { UpcommingVisitsService } from './upcomming-visits.service';

@Component({
  selector: 'app-upcomming-visits',
  templateUrl: './upcomming-visits.component.html',
})
export class UpcommingVisitsComponent implements OnInit {
  Servicelist: any = [];
  submitted: boolean = false;
  facilityForm: FormGroup;

  myChart: any = [];
  age: any = [];
  alldates: any = [];
  upcommingDates: any = [];
  upcommingdata: any = [];

  public upcommingVisitsData: UpcommingVisitsData = new UpcommingVisitsData();

  constructor(private router: Router,
    private _sendBuilSMSService: SendBuilSMSService,
    private fb: FormBuilder,
    private _addUserService: AddUserService,
    private _appointmentsService: AppointmentsService,
    private _upcommingVisitsService: UpcommingVisitsService,
  ) { }
  ngOnInit() {
    this.facilityForm = this.fb.group({
      servicePointId: [""],
    });
    this.UpcommingVisits();
    this.GetServicePoint();
  }
  UpcommingVisits() {
    this._appointmentsService.getUpcommingVisitsDetails().subscribe(
      res => {
        this.upcommingdata = [];
        this.age = [];
        this.alldates = [];
        this.upcommingDates = [];
        this.upcommingdata = res;
        this.upcommingdata.forEach(function (item, index) {
          this.age.push(item.age);
          this.alldates.push(item.visitDate);
        }.bind(this));
        this.alldates.forEach((date) => {
          let jsdate = new Date(date);
          const options = { year: 'numeric', month: 'long', day: 'numeric' };
          this.upcommingDates.push(jsdate.toLocaleDateString(undefined, options));
        })
        this.myChart = new Chart('canvas', {
          type: 'line',
          data: {
            labels: this.upcommingDates,
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
  show(event) {
    if (event.currentTarget.classList[1] === "upcommingvisitap") {
      var x = document.getElementById("upcommingvisitapp");
      if (x.style.display === "none") {
        return x.style.display = "block";
      }
    }
    return x.style.display = "none";
  }

  Submit() {
    this.submitted = true;   
    this.upcommingVisitsData.ServicePointId = Number(this.facilityForm.value.servicePointId);
    this._upcommingVisitsService.getUpcommingVisits(this.upcommingVisitsData).subscribe(
      res => {
        this.upcommingdata = [];
        this.age = [];
        this.alldates = [];
        this.upcommingDates = [];
        $('#chartType option:contains("Line")').prop('selected', true);
        $("#chartType").val("line");
        this.upcommingdata = res;
        this.upcommingdata.forEach(function (item, index) {
          this.age.push(item.age);
          //this.temp_fullname.push(item.fullName);
          this.alldates.push(item.visitDate);
        }.bind(this));
        this.alldates.forEach((date) => {
          let jsdate = new Date(date);
          const options = { year: 'numeric', month: 'long', day: 'numeric' };
          this.upcommingDates.push(jsdate.toLocaleDateString(undefined, options));
        })
        this.myChart.destroy();
        this.myChart = new Chart('canvas', {
          type: 'line',
          data: {
            labels: this.upcommingDates,
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
  updateVisitChartType(event) {
    this.myChart.destroy();
    this.myChart = new Chart('canvas', {
      type: event,
      data: {
        labels: this.upcommingDates,
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
export class UpcommingVisitsData {
  FacilityId: any = 0;
  ServicePointId: any = 0;
}
