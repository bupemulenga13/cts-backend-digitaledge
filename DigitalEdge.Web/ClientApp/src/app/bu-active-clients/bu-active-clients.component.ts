import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddUserService } from '../adduser/adduser.service';
import { AppointmentsService } from '../appointments/appointments.service';
import * as Chart from 'chart.js';
import { BUActiveClientsService } from './bu-active-clients.service';

@Component({
  selector: 'app-bu-active-clients',
  templateUrl: './bu-active-clients.component.html',
})
export class BUActiveClientsComponent implements OnInit {
  Servicelist: any = [];
  serviceDefault = 0;
  submitted: boolean = false;
  facilityForm: FormGroup;
  clientpastvisitdetails: any = [];
  clientPastMyChart: any = [];
  clientPast_age: any = [];
  clientPast_alldates: any = [];
  clientPast_visitDate: any = [];
  public buactiveClients: BUActiveClientsData = new BUActiveClientsData();

  constructor(private router: Router,
    private fb: FormBuilder,
    private _addUserService: AddUserService,
    private _appointmentsService: AppointmentsService,
    private _buActiveClientsService: BUActiveClientsService,
  ) { }
  ngOnInit() {
    this.facilityForm = this.fb.group({
      servicePointId: [""]    
    });      
    this.GetServicePoint();
    this.ViewVisitPastDetails();
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
  ViewVisitPastDetails() {
    this._appointmentsService.getClientVisitPastDetails().subscribe(
      res => {
        this.clientpastvisitdetails = res;
        this.clientpastvisitdetails.forEach(function (item, index) {
          this.clientPast_age.push(item.age);
          this.clientPast_alldates.push(item.visitDate);
        }.bind(this));
        this.clientPast_alldates.forEach((date) => {
          let jsdate = new Date(date);
          const options = { year: 'numeric', month: 'long', day: 'numeric' } as const;
          this.clientPast_visitDate.push(jsdate.toLocaleDateString(undefined, options));
        })
        this.clientPastMyChart = new Chart('clientPastcanvas', {
          type: 'line',
          data: {
            labels: this.clientPast_visitDate,
            datasets: [
              {
                label: "Age",
                fill: false,
                backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
                borderColor: 'black',
                data: this.clientPast_age,
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

  updateVisitPostChartType(event) {
    this.clientPastMyChart.destroy();
    this.clientPastMyChart = new Chart('clientPastcanvas', {
      type: event,
      data: {
        labels: this.clientPast_visitDate,
        datasets: [
          {
            label: "Age!",
            fill: false,
            backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
            borderColor: 'black',
            data: this.clientPast_age,
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


  show(event) {
    if (event.currentTarget.classList[1] === "activeclient") {
      var x = document.getElementById("activeclients");
      if (x.style.display === "none") {
        return x.style.display = "block";
      }
    }
    return x.style.display = "none";
    }
 
  Submit() {
    this.submitted = true;   
    this.buactiveClients.ServicePointId = Number(this.facilityForm.value.servicePointId);
    this._buActiveClientsService.getActiveClientFilter(this.buactiveClients).subscribe(
      res => {
        this.clientpastvisitdetails = [];
        this.clientPast_age = [];
        this.clientPast_alldates = [];
        this.clientPast_visitDate = [];
        $('#chartType option:contains("Line")').prop('selected', true);
        $("#chartType").val("line");  
        this.clientpastvisitdetails = res;
        this.clientpastvisitdetails.forEach(function (item, index) {
          this.clientPast_age.push(item.age);
            //this.temp_fullname.push(item.fullName);
          this.clientPast_alldates.push(item.visitDate);
          }.bind(this));
        this.clientPast_alldates.forEach((date) => {
          let jsdate = new Date(date);
          const options = { year: 'numeric', month: 'long', day: 'numeric' } as const;
          this.clientPast_visitDate.push(jsdate.toLocaleDateString(undefined, options));
          })
        this.clientPastMyChart.destroy();
        this.clientPastMyChart = new Chart('clientPastcanvas', {
            type: 'line',
            data: {
              labels: this.clientPast_visitDate,
              datasets: [
                {
                  label: "Age",
                  fill: false,
                  backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
                  borderColor: 'black',
                  data: this.clientPast_age,
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
}
export class BUActiveClientsData {
  ServicePointId: any = 0;
}
