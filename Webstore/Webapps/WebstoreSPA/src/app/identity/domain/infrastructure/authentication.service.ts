import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ILoginRequest } from '../models/login-request';
import { Observable } from 'rxjs';
import { ILoginResponse } from '../models/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private httpClient: HttpClient) { }

  public login(request: ILoginRequest): Observable<ILoginResponse>{
    return this.httpClient.post<ILoginResponse>("http://localhost:4000/api/v1/Authentication/Login", request);
  }
}
