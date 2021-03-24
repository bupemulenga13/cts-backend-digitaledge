import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class UpcommingAppointmentService {
 
  constructor(private _http: HttpClient) {
  }
  getUpcommingAppointmentFilter(user: any) {
    return this._http.post<any>('/api/Visit/ViewUpcommingAppointment', user, httpOptions);
  }
}
