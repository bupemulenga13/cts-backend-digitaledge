import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { FormGroup, FormBuilder } from '@angular/forms';
import { AddUserService } from '../adduser/adduser.service';
import { VisitHistoryService } from './visit-history.service';

@Component({
  selector: 'app-visit-history',
  templateUrl: './visit-history.component.html',
})
export class VisitHistoryComponent implements OnInit {
  Servicelist: any = [];
  submitted: boolean = false;
 serviceForm: FormGroup;
  vistData: any = [];

  public visitHistoryData: VisitHistoryData = new VisitHistoryData();

  constructor(private router: Router,
    private fb: FormBuilder,
    private _addUserService: AddUserService,
    private _visitHistoryService: VisitHistoryService,
  ) { }
  ngOnInit() {
    this.serviceForm = this.fb.group({
      servicePointId: [""],
    });
    this.VisitHistory();
    this.GetServicePoint();
  }
  VisitHistory() {
    this._visitHistoryService.getVisitHistoryDetails().subscribe(
      res => {
        this.vistData = [];
        this.vistData = res;       
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
  Submit() {
    this.submitted = true;
    this.visitHistoryData.ServicePointId = Number(this.serviceForm.value.servicePointId);
    if (this.visitHistoryData.ServicePointId === 0) {
      this.VisitHistory();
    }
    else {
      this._visitHistoryService.getVisitHistoryByService(this.visitHistoryData).subscribe(
        res => {
          this.vistData = [];
          this.vistData = res;
        }
      );
    }
  }
}
export class VisitHistoryData {
  ServicePointId: any = 0;
}
