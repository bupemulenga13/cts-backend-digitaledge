import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class ClientInfoService {
 
  constructor(private _http: HttpClient) {
  }  
  getClientDetails(model: any) {
    return this._http.post<any>('/api/Visit/GetClientInfo', model, httpOptions);
  }  
}
