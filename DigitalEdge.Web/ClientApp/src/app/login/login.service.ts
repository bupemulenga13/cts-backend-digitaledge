import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class LoginService {
  Url: string;
  token: string;
  constructor(private http: HttpClient) {  
  }
  Login(model: any) {
    return this.http.post<any>('/api/Account/Login', model);    
  } 
}  
