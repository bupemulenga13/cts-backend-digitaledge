import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { SendBuilSMSService } from '../send-bulk-sms/send-bulk-sms.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddUserService } from '../adduser/adduser.service';
import { AppointmentsService } from '../appointments/appointments.service';
import * as Chart from 'chart.js';
import { ActiveClientsService } from './active-clients.service';

@Component({
  selector: 'app-active-clients',
  templateUrl: './active-clients.component.html',
})
export class ActiveClientsComponent implements OnInit {
  provinceList: any = [];
  Servicelist: any = [];
  submitted: boolean = false;
  facilityForm: FormGroup;
  facilitys: any = [];
  facilityServicelist: any = [];
  districtList: any = [];

  clientpastvisitdetails: any = [];
  clientPastMyChart: any = [];
  clientPast_age: any = [];
  clientPast_alldates: any = [];
  clientPast_visitDate: any = [];
  public activeClients: ActiveClientsData = new ActiveClientsData();

  constructor(private router: Router,
    private _sendBuilSMSService: SendBuilSMSService,
    private fb: FormBuilder,
    private _addUserService: AddUserService,
    private _appointmentsService: AppointmentsService,
    private _activeClientsService: ActiveClientsService,
  ) { }
  ngOnInit() {
    this.facilityForm = this.fb.group({
      facilityId: [""],
      servicePointId: [""],
      provinceId: [""],
      districtId: [""],
    });      
    this.FacilityData();
    this.GetProvince();
    this.ViewVisitPastDetails();
  }
  FacilityData() {
    this._sendBuilSMSService.GetFacility().subscribe(data => {
      if (data.length > 0) {
        this.facilitys = data;       
      }
    });
  } 
  GetProvince() {
    this.provinceList = [];
    this._sendBuilSMSService.GetProvince().subscribe(provinceData => {
      if (provinceData.length > 0) {
        this.provinceList = provinceData;
        
      }
    });
  }
  GetDistrictId(id) {
    this.districtList = [];
    if (id !== "" && id!==null) {
      this._sendBuilSMSService.GetDistrict(id).subscribe(data => {
        if (data.length > 0) {
          this.districtList = data;
          this.facilityForm.get('provinceId').setValue(id, {
            onlySelf: true
          })
          this.facilityForm.get('districtId').setValue("", {
            onlySelf: true
          })
         
        }
      });
    }
  }
  GetServicePoint(e) {
    this.Servicelist = [];
    this._addUserService.getservicedata().subscribe(
      data => {
        if (data !== null && data!=="") {
          this.facilityServicelist = data;
          this.facilityServicelist.forEach(function (item, index) {
            if (item.facilityId === Number(this.facilityForm.value.facilityId)) {
              this.Servicelist.push(item);
            }
          }.bind(this));
          this.facilityForm.get('facilityId').setValue(e.target.value, {
            onlySelf: true
          })
          this.facilityForm.get('servicePointId').setValue("", {
            onlySelf: true
          })
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
          const options = {year: 'numeric', month: 'long', day: 'numeric' };
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
  get f() {
    return this.facilityForm.controls;
  }
  Submit() {
    this.submitted = true;    
    this.activeClients.FacilityId = Number(this.facilityForm.value.facilityId);
    this.activeClients.ServicePointId = Number(this.facilityForm.value.servicePointId);
    this.activeClients.ProvinceId = Number(this.facilityForm.value.provinceId);
    this.activeClients.DistrictId = Number(this.facilityForm.value.districtId);
    this._activeClientsService.getActiveClientFilter(this.activeClients).subscribe(
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
          this.clientPast_alldates.push(item.visitDate);
          }.bind(this));
        this.clientPast_alldates.forEach((date) => {
            let jsdate = new Date(date);
          const options = { year: 'numeric', month: 'long', day: 'numeric' };
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
export class ActiveClientsData {
  FacilityId: any = 0;
  ServicePointId: any = 0;
  ProvinceId: any = 0;
  DistrictId: any = 0; 
}
