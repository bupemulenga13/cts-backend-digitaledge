import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})


export class FacilityManagmentService {
 
  constructor(private _http: HttpClient) {
  }
  deleteFacility(user: any) {
    return this._http.post<any>('/api/Visit/DeleteFacility', user, httpOptions);
  }
  deleteServicePoint(user: any) {
    return this._http.post<any>('/api/Visit/DeleteServicePoint', user, httpOptions);
  }
}
