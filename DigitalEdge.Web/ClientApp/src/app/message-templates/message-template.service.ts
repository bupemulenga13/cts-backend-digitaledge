import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from '../app.configuration';
@Injectable({
  providedIn: 'root'
})
export class MessageTemplateService {

  constructor(private http: HttpClient, private _appConfig: AppConfig) { }

  GetTemplates() {
    return this.http.get<string>('api/Template/GetTemplates');

  }

  CreateTemplate(model) {
    return this.http.post<any>('api/Template/CreateTemplate', model)
  }

  DeleteTemplate(model) {
    return this.http.post<any>('api/Template/DeleteTemplate', model)
  }


}
