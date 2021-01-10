import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';



const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable()
export class AddUserService {

  constructor(private _http: HttpClient) {
  }
  getRoleList() {
    return this._http.get<any>('/api/Account/GetRoles')
  }
  getfacilitydata() {
    return this._http.get<any>('/api/Account/GetFacilityData')
  }
  getfacilityUserdata() {
    return this._http.get<any>('/api/Account/GetFacilityUserData')
  }
  getservicedata() {
    return this._http.get<any>('/api/Account/GetServiceData')
  }  
  getUserById(id: any) {
    return this._http.get<any>('/api/Account/GetData/' + id)
  }
  saveUser(model: any) {
    return this._http.post<any>('/api/Account/Create', model, httpOptions);
  }
  updateUser(model: any) {
    return this._http.put<any>('/api/Account/Edit', model, httpOptions);
  }
  deleteUser(user: any) {    
    return this._http.post<any>('/api/Account/Delete', user , httpOptions);
  }
  deleteFacilityUser(user: any) {
    return this._http.post<any>('/api/Account/DeleteFacility', user, httpOptions);
  }  
  savefacilityUser(model: any) {
    return this._http.post<any>('/api/Account/FacilityCreate', model, httpOptions);
  }  
  saveServicePoints(model: any) {
    return this._http.post<any>('/api/Account/ServicePointCreate', model, httpOptions);
  }
  editServicePoints(model: any) {
    return this._http.post<any>('/api/Account/ServicePointUpdate', model, httpOptions);
  }
  editfacilityUser(model: any) {
    return this._http.post<any>('/api/Account/UpdatefacilityUser', model, httpOptions);
  }
}
