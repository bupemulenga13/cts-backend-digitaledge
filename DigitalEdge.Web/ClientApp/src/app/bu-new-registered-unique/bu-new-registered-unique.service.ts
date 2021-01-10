import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class BUNewRegisteredUniqueService {
  
  constructor(private _http: HttpClient) {
  }
  getClientDetailsFilter(model) {
    return this._http.post<any>('/api/Visit/ViewClientDetailsFilters', model, httpOptions)
  } 
}
