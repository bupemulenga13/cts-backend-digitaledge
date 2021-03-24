import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
//import { SendBuilSMSService } from '../send-bulk-sms/send-bulk-sms.service';
import { FormGroup, FormBuilder} from '@angular/forms';
import { AddUserService } from '../adduser/adduser.service';
import { AppointmentsService } from '../appointments/appointments.service';
import * as Chart from 'chart.js';
import { MissedVisitsService } from './missed-visits.service';

@Component({
  selector: 'app-missed-visits',
  templateUrl: './missed-visits.component.html',
})
export class MissedVisitsComponent implements OnInit {
  Servicelist: any = [];
  submitted: boolean = false;
  facilityForm: FormGroup;
  myChart: any = [];
  age: any = [];
  allDates: any = [];
  missedData: any = [];
  missedDates: any = [];
  public missedVisitsData: MissedVisitsData = new MissedVisitsData();

  constructor(private router: Router,
    private fb: FormBuilder,
    private _addUserService: AddUserService,
    private _appointmentsService: AppointmentsService,
    private _missedVisitsService: MissedVisitsService,
  ) { }
  ngOnInit() {
    this.facilityForm = this.fb.group({
      servicePointId: [""],
    });
    this.GetServicePoint();
    this.MissedVisits();
  }

  MissedVisits() {
    this._appointmentsService.getMissedVisitsDetails().subscribe(
      data => {
        this.missedData = data;
        this.missedData.forEach(function (item, index) {
          this.age.push(item.age);
          this.allDates.push(item.priorAppointmentDate);
        }.bind(this));
        this.allDates.forEach((date) => {
          let jsdate = new Date(date);
          const options = { year: 'numeric', month: 'long', day: 'numeric' };
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
    if (event.currentTarget.classList[1] === "missedvisitap") {
      var x = document.getElementById("missedvisitapp");
      if (x.style.display === "none") {
        return x.style.display = "block";
      }
    }
    return x.style.display = "none";
  }

  Submit() {
    this.submitted = true;   
    this.missedVisitsData.ServicePointId = Number(this.facilityForm.value.servicePointId);
    this._missedVisitsService.getVisitsMissedFilter(this.missedVisitsData).subscribe(
      res => {
        this.missedData = [];
        this.age = [];
        this.allDates = [];
        this.missedDates = [];
        this.missedData = res;
        $('#chartType option:contains("Line")').prop('selected', true);
        $("#chartType").val("line");  
        this.missedData.forEach(function (item, index) {
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
  updateVisitMissedChartType(event) {
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
export class MissedVisitsData {
  ServicePointId: any = 0;
}
