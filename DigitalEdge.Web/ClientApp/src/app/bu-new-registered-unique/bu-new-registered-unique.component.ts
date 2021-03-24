import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { SendBuilSMSService } from '../send-bulk-sms/send-bulk-sms.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddUserService } from '../adduser/adduser.service';
import { AppointmentsService } from '../appointments/appointments.service';
import * as Chart from 'chart.js';
import { BUNewRegisteredUniqueService } from './bu-new-registered-unique.service';

@Component({
  selector: 'app-bu-new-registered-unique',
  templateUrl: './bu-new-registered-unique.component.html',
})
export class BUNewRegisteredUniqueComponent implements OnInit {
  provinceList: any = [];
  Servicelist: any = [];
  provinceDefault = 0;
  districtDefault = 0;
  facilityDefault = 0;
  serviceDefault = 0;
  submitted: boolean = false;
  facilityForm: FormGroup;
  facilitys: any = [];
  facilityServicelist: any = [];
  districtList: any = [];
  clientdetails: any = [];
  clientMyChart: any = [];
  client_age: any = [];
  client_alldates: any = [];
  client_visitDate: any = [];

  public buNewClientsData: BUNewClientsData = new BUNewClientsData();

  constructor(private router: Router,
    private _sendBuilSMSService: SendBuilSMSService,
    private fb: FormBuilder,
    private _addUserService: AddUserService,
    private _appointmentsService: AppointmentsService,
    private _bunewRegisteredUniqueService: BUNewRegisteredUniqueService,
  ) { }
  ngOnInit() {
    this.facilityForm = this.fb.group({
      servicePointId: [""],
    });
    this.GetServicePoint();
    this.ViewClients();
  }
  GetServicePoint() {
    this.Servicelist = [];
    this._addUserService.getservicedata().subscribe(
      data => {
        if (data !== null) {
          this.Servicelist = data;
          //this.facilityServicelist = data;
          //this.facilityServicelist.forEach(function (item, index) {
          //  if (item.facilityId === Number(this.facilityForm.value.facilityId)) {
          //    this.Servicelist.push(item);
          //  }
          //}.bind(this));
          //this.facilityDefault = 0;
          //this.serviceDefault = 0;
        }
      },
    );
  }
  ViewClients() {
    this._appointmentsService.getClientDetails().subscribe(
      res => {
        this.clientdetails = res;
        this.clientdetails.forEach(function (item, index) {
          this.client_age.push(item.age);
          this.client_alldates.push(item.visitDate);
        }.bind(this));
        this.client_alldates.forEach((date) => {
          let jsdate = new Date(date);
          const options = { year: 'numeric', month: 'long', day: 'numeric' };
          this.client_visitDate.push(jsdate.toLocaleDateString(undefined, options));
        })
        this.clientMyChart = new Chart('clientuniquecanvas', {
          type: 'line',
          data: {
            labels: this.client_visitDate,
            datasets: [
              {
                label: "Age",
                fill: false,
                backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
                borderColor: 'black',
                data: this.client_age,
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

  updateClientChartType(event) {
    this.clientMyChart.destroy();
    this.clientMyChart = new Chart('clientuniquecanvas', {
      type: event,
      data: {
        labels: this.client_visitDate,
        datasets: [
          {
            label: "Age!",
            fill: false,
            backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
            borderColor: 'black',
            data: this.client_age,
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
    if (event.currentTarget.classList[1] === "uniqueclient") {
      var x = document.getElementById("uniqueclients");
      if (x.style.display === "none") {
        return x.style.display = "block";
      }
    } 
      return x.style.display = "none";
    }


  Submit() {
    this.submitted = true;    
    this.buNewClientsData.ServicePointId = Number(this.facilityForm.value.servicePointId);
    this._bunewRegisteredUniqueService.getClientDetailsFilter(this.buNewClientsData).subscribe(
      res => {
        this.clientdetails = [];
        this.client_age = [];
        this.client_alldates = [];
        this.client_visitDate = [];
        $('#chartType option:contains("Line")').prop('selected', true);
        $("#chartType").val("line");  
          this.clientdetails = res;
          this.clientdetails.forEach(function (item, index) {
            this.client_age.push(item.age);
            this.client_alldates.push(item.visitDate);
          }.bind(this));
          this.client_alldates.forEach((date) => {
            let jsdate = new Date(date);
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            this.client_visitDate.push(jsdate.toLocaleDateString(undefined, options));
          })
        this.clientMyChart.destroy();
          this.clientMyChart = new Chart('clientuniquecanvas', {
            type: 'line',
            data: {
              labels: this.client_visitDate,
              datasets: [
                {
                  label: "Age",
                  fill: false,
                  backgroundColor: ['#ff0000', '#ff4000', '#ff8000', '#ffbf00', '#ffbf00', '#ffff00', '#bfff00', '#80ff00', '#40ff00', '#00ff00'],
                  borderColor: 'black',
                  data: this.client_age,
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
export class BUNewClientsData {
  ServicePointId: any = 0;  
}
