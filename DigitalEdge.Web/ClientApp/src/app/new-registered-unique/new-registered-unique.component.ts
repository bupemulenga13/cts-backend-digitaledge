import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { SendBuilSMSService } from '../send-bulk-sms/send-bulk-sms.service';
import { FormGroup, FormBuilder} from '@angular/forms';
import { AddUserService } from '../adduser/adduser.service';
import { AppointmentsService } from '../appointments/appointments.service';
import { Chart } from 'chart.js';  

import { NewRegisteredUniqueService } from './new-registered-unique.service';

@Component({
  selector: 'app-new-registered-unique',
  templateUrl: './new-registered-unique.component.html',
})
export class NewRegisteredUniqueComponent implements OnInit {
  provinceList: any = [];
  Servicelist: any = [];
  submitted: boolean = false;
  facilityForm: FormGroup;
  facilitys: any = [];
  facilityServicelist: any = [];
  districtList: any = [];
  clientdetails: any = [];
  clientMyChart: any= [];
  client_age: any = [];
  client_alldates: any = [];
  client_visitDate: any = [];

  public dataClients: NewClientsData = new NewClientsData();

  constructor(private router: Router,
    private _sendBuilSMSService: SendBuilSMSService,
    private fb: FormBuilder,
    private _addUserService: AddUserService,
    private _appointmentsService: AppointmentsService,
    private _newRegisteredUniqueService: NewRegisteredUniqueService,
  ) { }
  ngOnInit() {
   
    this.facilityForm = this.fb.group({
      facilityId: [""],
      servicePointId: [""],
      provinceId: [""],
      districtId: [""],
    });
    this.ViewClients();
    this.FacilityData();
    this.GetProvince();
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
    if (id !== "" && id !==null) {
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
  ViewClients() {
    this._appointmentsService.getClientDetails().subscribe(
      res => {
        this.clientdetails = res;
        this.clientdetails.forEach(function (item, index) {
          this.client_age.push(item.age);
          this.client_alldates.push(item.appointmentDate);
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
    this.dataClients.FacilityId = Number(this.facilityForm.value.facilityId);
    this.dataClients.ServicePointId = Number(this.facilityForm.value.servicePointId);
    this.dataClients.ProvinceId = Number(this.facilityForm.value.provinceId);
    this.dataClients.DistrictId = Number(this.facilityForm.value.districtId);
    this._newRegisteredUniqueService.getClientDetailsFilter(this.dataClients).subscribe(
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
            const options = {year: 'numeric', month: 'long' };
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
export class NewClientsData {
  FacilityId: any = 0;
  ServicePointId: any = 0;
  ProvinceId: any = 0;
  DistrictId: any = 0; 
}
