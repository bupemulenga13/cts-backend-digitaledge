import { Component, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import * as jQuery from 'jquery';
import { NgxFileDropEntry, FileSystemFileEntry } from 'ngx-file-drop';
import { FormGroup, FormBuilder } from '@angular/forms';
import { SendBuilSMSService } from './send-bulk-sms.service';
import { messageTemplateModel } from '../message-templates/message-templates.component';


@Component({
    selector: 'app-send-bulk-sms',
    templateUrl: './send-bulk-sms.component.html',
})
export class SendBulkSMSComponent {
    userForm: FormGroup;
    filename: any;
    name: any;
    sendSmsData: any;
    FacilityList: any[];
    ServicePointList: any[];
    ImportList: any;
    hasMessage: boolean = true;
    hasTime: boolean = false;s
    hasTemplate: boolean = false;
    fileError: string = "";
    checkValidation: string = "";
  AdhocCase: boolean = true;
  emptyType: boolean = false;
  emptyLanguage: boolean = false;
  emptyDate: boolean = false;
  emptyService: boolean = false;
  emptyTimed: boolean = false;
  emptyMessage: boolean = false;
    public messageTemplate: messageTemplateModel = new messageTemplateModel();

    public sendBulkMessage: SendBulkMessage = new SendBulkMessage();


    public selectType: any[] = [
        { name: 'Appointment', value: 'Appointment', checked:true },
        { name: 'Reminder', value: 'Reminder' },
        { name: 'Adhoc Message', value: 'Adhoc Message' }
    ]

    public selectLanguage: any[] = [
        { text: 'English', name: 'English', value: 'English', isSelected: false },
        { text: 'Chitonga', name: 'Chitonga', value: 'Chitonga', isSelected: false },
        { text: 'Bemba', name: 'Bemba', value: 'Bemba', isSelected: false },
        { text: 'Nyanja', name: 'Nyanja', value: 'Nyanja', isSelected: false },
    ];

    public insertShortCode: any[] = [
        { text: 'SelectParameter', name: 'SelectParameter', value: 0, isHeading: true },
        { text: '{Facility_Name}', name: 'Facility Name', value: 1, isSelected: false },
        { text: '{Date}', name: 'Date', value: 2, isHeading: false, isSelected: false },
        { text: '{Time}', name: 'Time', value: 3, isHeading: false, isSelected: false },
        { text: '{Client_Name}', name: 'Client Name', value: 4, isHeading: false, isSelected: false },
        { text: '{Facility_Contact_Number}', name: 'Facility Contact Number', value: 5, isHeading: false, isSelected: false },
        { text: '{Source_Point_Name}', name: 'Source Point Name', value: 6, isHeading: false, isSelected: false }

    ];

    constructor(private router: Router,
        private toastr: ToastrService,
        private SpinnerService: NgxSpinnerService,
        private _fb: FormBuilder,
        private _sendBuilSMSService: SendBuilSMSService,
        private _elem: ElementRef
    ) {
        this.userForm = this._fb.group({
            message: ['', ''],
        })
    }
    ngOnInit() {
        this.messageTemplate.Type = "Appointment";
       // this.GetMessage();
        this.GetFacility();
    }
    placeShortCodeInMessage(shortCodeIndex: number) {
        this.insertShortCode[shortCodeIndex].isSelected = true;
        this.messageTemplate.Message += ' ' + this.insertShortCode[shortCodeIndex].text;
        this.sendBulkMessage.Message = this.messageTemplate.Message;
    }

    GetType(type) {
        this.messageTemplate.Type = type;
        if (this.messageTemplate.Type == "Adhoc Message") {
            this.AdhocCase = false;
            this.GetMessage();
        }
        else {
            this.AdhocCase = true;
            this.hasMessage = true;
            this.hasTemplate = false;
        }
        if (this.messageTemplate.Language != "" && this.messageTemplate.Type != "") {
            this.GetMessage();
        }
    }
  GetLanguage(language) {
 
        this.AdhocCase = true;
        this.messageTemplate.Language = language;
        if (this.messageTemplate.Language != "" && this.messageTemplate.Type != "") {
            this.GetMessage();
        }
    }

    GetFacility() {
        this._sendBuilSMSService.GetFacility().subscribe(data => {
            if (data.length > 0) {
                this.FacilityList = data;
            }
        });
    }
  errorValidation() {
    this.sendBulkMessage.Facility == "" ? this.emptyType = true : this.emptyType = false;
    this.sendBulkMessage.SevicePoint == "0" ? this.emptyService = true : this.emptyService = false;
    this.sendBulkMessage.AppointmentDate == "" ? this.emptyDate = true : this.emptyDate = false;
    this.messageTemplate.Language == "" ? this.emptyLanguage = true : this.emptyLanguage = false;
    if (this.AdhocCase == true) {
      this.sendBulkMessage.Message == "" ? this.emptyMessage = false : this.emptyMessage = false;
    }
    else {
      this.sendBulkMessage.Message == "" ? this.emptyMessage = true : this.emptyMessage = true;
    }
    if (this.hasTime) {
      this.sendBulkMessage.AppointmentTime == "" ? this.emptyTimed = true : this.emptyTimed = false;
    }
  }
    GetServicePointId(value) {
      if (value != undefined && value != "") {
        var index = this.FacilityList.findIndex(x => x.facilityId == Number(value));
        this.sendBulkMessage.Facility = this.FacilityList[index].facilityName;
        this.sendBulkMessage.FacilityId = this.FacilityList[index].facilityId;
        this.GetServicePoint(Number(value));
      }
      
    }


  GetServicePointName(value) {
    var index = this.ServicePointList.findIndex(x => x.servicePointId == Number(value));
         this.sendBulkMessage.SevicePoint = this.ServicePointList[index].servicePointName;
        this.sendBulkMessage.ServicePointName = this.ServicePointList[index].servicePointName;
        this.sendBulkMessage.ServicePointId = this.ServicePointList[index].servicePointId;
    }

  GetServicePoint(id) {
        this.ServicePointList = [];
        this._sendBuilSMSService.GetServicePoint(id).subscribe(servicePointData => {
            if (servicePointData.length > 0) {
                this.ServicePointList = servicePointData;
                var servicePointValue = this._elem.nativeElement.querySelector('#service_Point');
              if (servicePointValue != undefined && servicePointValue.value != "Select Service Point") {
                this.sendBulkMessage.SevicePoint = servicePointValue.value;
                this.sendBulkMessage.ServicePointName = this.ServicePointList[0].servicePointName;
                this.sendBulkMessage.ServicePointId = this.ServicePointList[0].servicePointId;
              }              
            }
        });
    }
    GetAdhocMessage(message) {
        if (this.messageTemplate.Type == 'Adhoc Message') {
            this.messageTemplate.Message = message;
            this.sendBulkMessage.Message = message;
        }
           
    }
  GetMessage() {
        if (this.messageTemplate.Type == 'Adhoc Message') {
            this.messageTemplate.Message = '';
            this.sendBulkMessage.Message = '';
            this.hasMessage = false;
            this.hasTemplate = true;
        }
        else {
            this._sendBuilSMSService.GetMessage(this.messageTemplate).subscribe(message => {
                if (message != null) {
                    this.messageTemplate.Message = message.message;
                    this.sendBulkMessage.Message = message.message;
                }
                else {
                    this.messageTemplate.Message = '';
                    this.sendBulkMessage.Message = '';
                }
                this.hasMessage = true;
                this.hasTemplate = false;
            })
        }

    }
    GetDate() {
        var dateValue = this._elem.nativeElement.querySelector('#myDate');
        if (dateValue != undefined) {
            this.sendBulkMessage.AppointmentDate = dateValue.value;
        }

    }
    GetTime() {
        var timeValue = this._elem.nativeElement.querySelector('#myTime');
        if (timeValue != undefined) {
            this.sendBulkMessage.AppointmentTime = timeValue.value;
        }

    }


    HasTime() {
        var HasTimeValue = this._elem.nativeElement.querySelector('#HasTime');
        if (HasTimeValue != undefined) {
            HasTimeValue.checked ? this.hasTime = true : this.hasTime = false;
        }
    }
    submit() {
        if (this.files != undefined) {
            this.fileError = "";
            this.sendBulkMessage.FileToUpload = this.files;
            this._sendBuilSMSService.ImportCSVData(this.files, this.userForm.value.message).subscribe(
                data => {
                    if (data != null && data != undefined) {
                        this.ImportList = data;
                    }
                })
        }
        else {
            this.fileError = "Please select atleast one file";
        }

      
    }
    public files: any;
    public dropped(files: NgxFileDropEntry[]) {
        this.fileError = "";
        if (files.length > 0) {
            for (const droppedFile of files) {
                const splitfilename = droppedFile.relativePath.split('.');
                const fileType = splitfilename[splitfilename.length - 1];
                if (fileType == "csv") {
                    if (droppedFile.fileEntry.isFile) {
                        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
                        fileEntry.file((file: File) => {
                            this.filename = droppedFile.relativePath;
                            this.files = file;
                            this.submit();
                        });
                        
                    } else {
                        // const fileEntry = droppedFile.fileEntry as FileSystemDirectoryEntry;
                    }
                }
                else {
                    this.fileError = "Please select only csv file.";
                }
            }
        }
        else {
            this.fileError = "Please select atleast one file";
        }
    }

    GetBulkMessage() {
        if (this.files != undefined) {
            this.CheckValidations();
            if (this.checkValidation=="") {
                this._sendBuilSMSService.GetBulkMessage(this.sendBulkMessage).subscribe(response => {
                    if (response.message == 'Messages sent') {
                        this.toastr.success('Send Successfully', '', {
                            timeOut: 3000
                        });
                        this.ClearRecords();
                    }
                    else {
                        this.toastr.error(response.message, '', {
                            timeOut: 3000
                        });
                        this.ClearRecords();
                    }
                });
            }
            else {
              this.errorValidation();              
            }
            
        }
        else {
            this.fileError = "Please select .csv file";
        }
    }
  CheckValidations() {
        this.checkValidation = "";
        if (this.AdhocCase) {
            if (this.sendBulkMessage.Facility == "") {
                this.checkValidation = "Facility And Service Point Name";
                return this.checkValidation;
            }
            if (this.sendBulkMessage.AppointmentDate == "") {
                this.checkValidation = "Appointment Date";
                return this.checkValidation;
            }
        }
            if (this.sendBulkMessage.Message == "") {
                this.checkValidation = "Message";
                return this.checkValidation;
            }

        return this.checkValidation;
    }
    ClearRecords()
    {
      this.checkValidation = '';
      this.emptyType = false;
      this.emptyLanguage = false;
      this.emptyTimed = false;
      this.emptyService = false;
      this.emptyMessage = false;
      this.emptyDate = false;
      this.sendBulkMessage = new SendBulkMessage();
      this.messageTemplate = new messageTemplateModel();
        this.messageTemplate.Type = 'Appointment';
        this.messageTemplate.Message = '';
        this.AdhocCase = true;
        this.files = null;
        this.filename = null;
        this.name = null;
        this.sendSmsData = null;
        this.ServicePointList = null;
        this.ImportList = null;
        this.hasMessage = true;
        this.hasTime = false;
        this.hasTemplate = false;
        this.messageTemplate.Language = '';
        this._elem.nativeElement.querySelector('#messages').value = '';
        this._elem.nativeElement.querySelector('#messageTyped').checked = true; 
    }
    public fileOver(event) {
        console.log(event);
    }
    public fileLeave(event) {
        console.log(event);
    }
}

export class SendBulkMessage {
    Facility: string = '';
    FacilityId: string = '';
    ServicePointName: string = '';
    ServicePointId: string = '';
    AppointmentDate: string = '';
    AppointmentTime: string = '';
    Message: string = '';
    SevicePoint: string = '0';
    FileToUpload: File;
}


