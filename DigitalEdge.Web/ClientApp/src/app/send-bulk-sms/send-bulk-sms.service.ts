import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class SendBuilSMSService {

  constructor(private _http: HttpClient) {  
  }


  ImportCSVData(files: File, message: any) {
    const formData: FormData = new FormData();
    formData.append("FileToUpload", files);
    formData.append("Message", message);
    return this._http.post<any>('/api/Visit/ImportCSVData', formData);
  }

  GetBulkMessage(model) {
    const formData: FormData = new FormData();
    formData.append("FileToUpload", model.FileToUpload);
    formData.append("Message", model.Message);
      formData.append("Facility", model.Facility);
      formData.append("FacilityId", model.FacilityId);
      formData.append("ServicePointName", model.ServicePointName);
      formData.append("ServicePointId", model.ServicePointId);
    formData.append("AppointmentDate", model.AppointmentDate);
    formData.append("AppointmentTime", model.AppointmentTime);
    return this._http.post<any>('/api/Visit/GetBulkMessage', formData);
  }

  GetFacility() {
    return this._http.get<any>('api/Visit/GetFacility');
  }

  GetServicePoint(id) {
    return this._http.get<any>('api/Visit/GetServicePoint/' + id);
  }

  GetMessage(model) {
    return this._http.post<any>('api/Visit/GetMessage', model)
  }
    GetDistrict(id) {
    return this._http.get<any>('api/Visit/GetDistrict/' + id);
  }
  GetProvince() {
    return this._http.get<any>('api/Visit/GetProvince');
  }
}
