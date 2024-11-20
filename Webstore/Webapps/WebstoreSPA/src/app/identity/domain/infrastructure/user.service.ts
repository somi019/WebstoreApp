import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserDetails } from '../models/user-details';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient : HttpClient) { }

  public getUserDetails(username: string) : Observable<IUserDetails>{
    // switchMap originalni observable (Observable<IAppState ) u neki drugi (Observable<IUserDetails>)
    // return this.appStateService.getAppState().pipe(
    //   take(1),
    //   switchMap((appState : IAppState) =>{
    //     const accessToken: string | undefined = appState.accessToken;

    //     const headers: HttpHeaders = new HttpHeaders().append("Authorization", `Bearer ${accessToken}`);

    //     return this.httpClient.get<IUserDetails>(`http://localhost:4000/api/v1/User/${username}`, { headers });
    //   })
    // );
    return this.httpClient.get<IUserDetails>(`http://localhost:4000/api/v1/User/${username}`);

    // All of this code was changed due to adding an interceptor which adds a http header Authorization
    // now we have it simplified in our services, now we don't need appService injection in the constructor
  }
}
