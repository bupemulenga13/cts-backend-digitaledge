import { Component, OnInit, TemplateRef } from '@angular/core';
import { MessageTemplateService } from './message-template.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-message-templates',
  templateUrl: './message-templates.component.html',
  styleUrls: ['./message-templates.component.css']
})
export class MessageTemplatesComponent implements OnInit {
  modalRef: BsModalRef;
  public messageTemplate: messageTemplateModel = new messageTemplateModel();
  messageTemplateList: any;
  emptyType: boolean = false;
  emptyLanguage: boolean = false;
  emptyMessage: boolean = false;
  emptyTimed: boolean = false;
  emptyStatus: boolean = false;
  NewCreate: boolean = false;
  ErrorMessage: string = "";

  public insertShortCode: any[] = [
    { text: 'SelectParameter', name: 'SelectParameter', value: 0, isHeading: true },
    { text: '{Facility_Name}', name: 'Facility Name', value: 1, isSelected: false },
    { text: '{Date}', name: 'Date', value: 2, isHeading: false, isSelected: false },
    { text: '{Time}', name: 'Time', value: 3, isHeading: false, isSelected: false },
    { text: '{Client_Name}', name: 'Client Name', value: 4, isHeading: false, isSelected: false },
    { text: '{Facility_Contact_Number}', name: 'Facility Contact Number', value: 5, isHeading: false, isSelected: false }
  ]

  public selectType: any[] = [
    { text: 'Appointment', name: 'Appointment', value: 'Appointment', isSelected: false },
    { text: 'Remainder', name: 'Reminder', value: 'Reminder', isSelected: false },
    { text: 'Adhoc Message', name: 'Adhoc Message', value: 'Adhoc Message', isSelected: false }
  ]

  public selectTimed: any[] = [
    { text: 'True', name: 'True', value: true, isSelected: false },
    { text: 'False', name: 'False', value: false, isSelected: false }
  ]

  public selectStatus: any[] = [
    { text: 'True', name: 'True', value: true, isSelected: false },
    { text: 'False', name: 'False', value: false, isSelected: false }
  ]

  public selectLanguage: any[] = [
    { text: 'English', name: 'English', value: 'English', isSelected: false },
    { text: 'Chitonga', name: 'Chitonga', value: 'Chitonga', isSelected: false },
    { text: 'Bemba', name: 'Bemba', value: 'Bemba', isSelected: false },
    { text: 'Nyanja', name: 'Nyanja', value: 'Nyanja', isSelected: false },
  ]

    constructor(private _messageTemplateService: MessageTemplateService,
        private toastr: ToastrService,
        private SpinnerService: NgxSpinnerService,public modalService: BsModalService) { }

  ngOnInit() {
    this.modalService.onHide.subscribe((e) => {
      console.log('close', this.modalService.config.initialState);
    });

    this.GetTemplate();
    this.messageTemplate = new messageTemplateModel();
  }

    openModal(template: TemplateRef<any>, added) {
    this.ErrorMessage = "";
    this.modalRef = this.modalService.show(template);
    if (added) {
      this.NewCreate = true;
      this.messageTemplate = new messageTemplateModel();
      this.emptyLanguage = false;
      this.emptyMessage = false;
      this.emptyType = false;
      this.selectTimed[0].isSelected = true;
      this.selectTimed[1].isSelected = false;
      this.selectStatus[0].isSelected = true;
      this.selectStatus[1].isSelected = false;
      const typeIndex = this.selectType.findIndex(x => x.isSelected);
      if (typeIndex != -1) {
        this.selectType[typeIndex].isSelected = false;
      }
      const languageIndex = this.selectLanguage.findIndex(x => x.isSelected);
      if (languageIndex != -1) {
        this.selectLanguage[languageIndex].isSelected = false;
      }

    }
    else {
      this.NewCreate = false;
    }
  }

  placeShortCodeInMessage(shortCodeIndex: number) {
    this.insertShortCode[shortCodeIndex].isSelected = true;
    this.messageTemplate.Message += ' ' + this.insertShortCode[shortCodeIndex].text;
  }

  errorValidation() {
    this.messageTemplate.Language == "" ? this.emptyLanguage = true : this.emptyLanguage = false;
    this.messageTemplate.Message.trim() == "" ? this.emptyMessage = true : this.emptyMessage = false;
    this.messageTemplate.Type == "" ? this.emptyType = true : this.emptyType = false;
  }

  GetMessage() {
    this.messageTemplate.Language == "" ? this.emptyLanguage = true : this.emptyLanguage = false;
    this.messageTemplate.Message.trim() == "" ? this.emptyMessage = true : this.emptyMessage = false;
    this.messageTemplate.Type == "" ? this.emptyType = true : this.emptyType = false;
  }

    GetTimed(timed) {
    timed == 'true' ? this.messageTemplate.Timed = true : this.messageTemplate.Timed = false;
    this.errorValidation();
  }

  GetType(type) {
    this.messageTemplate.Type = type;
    this.errorValidation();
  }
  GetLanguage(language) {
    this.messageTemplate.Language = language;
    this.errorValidation();
  }

  GetAddStatusValue(status) {
    status == 'true' ? this.messageTemplate.Status = true : this.messageTemplate.Status = false;
  }


  AddMessageTemplate() {
    this.ErrorMessage = "";
    const active = true;
    if (this.messageTemplate.Language != "" && this.messageTemplate.Message != "" && this.messageTemplate.Type != "") {
      if (this.messageTemplateList == undefined) {
        this._messageTemplateService.CreateTemplate(this.messageTemplate).subscribe(data => {
          this.closePopup();
            this.toastr.success('MessageTemplate save successfully !', '', {
                timeOut: 3000
            });
          this.GetTemplate();

        })
      }
      else {
        var Language_Type: any;
        if (this.messageTemplate.Status == false) {
          Language_Type = -1;
        }
        else {
          if (this.messageTemplate.MessageTemplateId == 0) {
            Language_Type = this.messageTemplateList.findIndex(x => x.language == this.messageTemplate.Language && x.type == this.messageTemplate.Type && x.status == this.messageTemplate.Status);
          }
          else {
            Language_Type = this.messageTemplateList.findIndex(x => x.language == this.messageTemplate.Language && x.type == this.messageTemplate.Type && x.status == this.messageTemplate.Status && x.messageTemplateId != this.messageTemplate.MessageTemplateId);
          }
        }
        if (Language_Type == -1) {
          this._messageTemplateService.CreateTemplate(this.messageTemplate).subscribe(data => {
              this.closePopup();
              this.toastr.success('MessageTemplate save successfully !', '', {
                  timeOut: 3000
              });
            this.GetTemplate();

          })
        }
        else {
          this.ErrorMessage = "Language and Type same name already active in MessageTemplateList"
        }
      }     
    }
    else {
      this.errorValidation();
    }

  }

  closePopup() {
    this.modalRef.hide();
    this.messageTemplate = new messageTemplateModel();
  }


  GetTemplate() {
    this._messageTemplateService.GetTemplates().subscribe(data => {
            this.messageTemplateList = data;
    });

  }

  EditMessageTemplate(data, template) {
    this.resetLanguageValues();
    const typeIndex = this.selectType.findIndex(x => x.value === data.type);
    if (typeIndex != -1) {
      this.selectType[typeIndex].isSelected = true;
      this.messageTemplate.Type = this.selectType[typeIndex].value;
    }
    const languageIndex = this.selectLanguage.findIndex(x => x.value === data.language);
    if (languageIndex != -1) {
      this.selectLanguage[languageIndex].isSelected = true;
      this.messageTemplate.Language = this.selectLanguage[languageIndex].value;
    }
    if (data.timed) {
      this.selectTimed[0].isSelected = true; this.selectTimed[0].value = true; this.messageTemplate.Timed = true;
    }
    else {
      this.selectTimed[1].isSelected = true; this.selectTimed[1].value = false; this.messageTemplate.Timed = false;
    }
    if (data.status) {
      this.selectStatus[0].isSelected = true; this.selectStatus[0].value = true; this.messageTemplate.Status = true;
    }
    else {
      this.selectStatus[1].isSelected = true; this.selectStatus[1].value = false; this.messageTemplate.Status = false;
    }

    this.messageTemplate.Message = data.message;
    this.messageTemplate.MessageTemplateId = data.messageTemplateId;
    this.errorValidation();
    this.openModal(template, false);
  }

  resetLanguageValues() {
    this.selectLanguage.forEach(languageValue => {
      languageValue.isSelected = false;
    });
    this.selectType.forEach(typeValue => {
      typeValue.isSelected = false;
    });
    this.selectTimed.forEach(timedValue => {
      timedValue.isSelected = false;
    });
      this.selectStatus.forEach(statusValue => {
          statusValue.isSelected = false;
      });
  }

  DeleteMessageTemplate() {
    ////this.messageTemplate = data;
    this._messageTemplateService.DeleteTemplate(this.messageTemplate).subscribe(data => {
        this.toastr.success('MessageTemplate delete successfully !', '', {
            timeOut: 1000
        });
      this.GetTemplate();
      this.modalRef.hide();

    })
  }
  deleteMessageTemplatePopup(modalDefault: TemplateRef<any>, data) {
    this.modalRef = this.modalService.show(modalDefault);

    this.messageTemplate = data;
  }
  CancelDelete(modalDefault) {
    this.modalRef.hide();
  }
}

export class messageTemplateModel {
  MessageTemplateId: number = 0;
  Type: string = '';
  Language: string = '';
  Message: string = '';
  Timed: boolean = true;
  Status: boolean = true;
}

