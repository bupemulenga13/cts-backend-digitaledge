import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators} from '@angular/forms';
import { ClientInfoService } from './client-info.service';


@Component({
  selector: 'app-client-info',
  templateUrl: './client-info.component.html',
})
export class ClientInfoComponent implements OnInit {
  clientData: any = [];
  isSubmitted: boolean = false;
  clientForm: FormGroup;


  constructor(private router: Router,
    private fb: FormBuilder,
    private _clientInfoService: ClientInfoService,
  ) { }
  ngOnInit() {
    this.clientForm = this.fb.group({
      firstName: ["", Validators.required],
      
    });
  }  
  get g() {
    return this.clientForm.controls;
  }
  Submit() {
    this.isSubmitted = true;
    const model = this.clientForm.value;
    if (this.clientForm.invalid) {
      return;
    }
    this._clientInfoService.getClientDetails(model).subscribe(
      res => {
        debugger;
        this.clientData = res;
      });
  }
}

