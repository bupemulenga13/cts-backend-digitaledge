import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})


export class VisitHistoryService {
 
  constructor(private _http: HttpClient) {
  }
  getVisitHistoryByService(user: any) {
    return this._http.post<any>('/api/Visit/GetVisitHistoryByServicePoint', user, httpOptions);
  }
  getVisitHistoryDetails() {
    return this._http.get<string>('/api/Visit/GetVisitHistory')
  }
}
