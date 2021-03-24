import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({
  providedIn: 'root'
})
export class AppointmentsService {
  Url: string;
  token: string;
  constructor(private http: HttpClient) {  
  }
  getAppointmentDetails() {
    return this.http.get<string>('/api/Visit/GetAppointmentsDetails')
  } 
  getUpcommingVisitsDetails() {
    return this.http.get<string>('/api/Visit/GetUpcommingVisitsDetails')
  }
  getMissedVisitsDetails() {
    return this.http.get<string>('/api/Visit/GetMissedVisitsDetails')
  }
  getAppointmentDetailsMissed() {
    return this.http.get<string>('/api/Visit/GetAppointmentsDetailsMissed')
  }
  viewDetails(appointmentId: any) {
    return this.http.get<string>('/api/Visit/ViewDetails/' + appointmentId)
  }
  getClientDetails() {
    return this.http.get<string>('/api/Visit/ViewClientDetails')
  } 
  getClientVisitPastDetails() {
    return this.http.get<string>('/api/Visit/ClientVisitPastDetails')
  }
  SendReminder(appointment: any) {
        return this.http.post<any>('/api/Visit/SendReminder', appointment, httpOptions)
  }
  saveAppointmentUser(appointment: any) {    
    return this.http.post<any>('/api/Account/CreateAppointment', appointment, httpOptions)
  } 
  }  
