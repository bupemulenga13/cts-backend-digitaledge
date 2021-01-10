import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class DashboardService {
  Url: string;
  token: string;
  constructor(private http: HttpClient) {
  }
  Getdetails() {
    return this.http.get<string>('/api/Account/GetData')
  } 
}
