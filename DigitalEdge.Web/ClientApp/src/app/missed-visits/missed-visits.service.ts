import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})


export class MissedVisitsService {
 
  constructor(private _http: HttpClient) {
  }
  getVisitsMissedFilter(user: any) {
    return this._http.post<any>('/api/Visit/ViewVisitsMissedFilter', user, httpOptions);
  }
}
