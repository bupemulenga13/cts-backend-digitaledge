import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class AppointmentCheckService {

  constructor(private _http: HttpClient) {
  } 
  getAppointmentCheck(model) {
    return this._http.post<any>('/api/Visit/GetAppointmentsCheck', model, httpOptions)
  }
}
