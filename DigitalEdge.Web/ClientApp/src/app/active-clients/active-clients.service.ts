import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})


export class ActiveClientsService {
 
  constructor(private _http: HttpClient) {
  }
  getActiveClientFilter(activeclients: any) {
    return this._http.post<any>('/api/Visit/ViewActiveClientFilter', activeclients, httpOptions);
  }
 
}
