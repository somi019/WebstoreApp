import { Injectable } from '@angular/core';
import { AuthenticationService } from '../infrastructure/authentication.service';
import { ILoginRequest } from '../models/login-request';
import { catchError, map, Observable, of } from 'rxjs';
import { ILoginResponse } from '../models/login-response';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';
import { log } from 'console';
import { JwtService } from 'src/app/shared/jwt/jwt.service';
import { JwtPayloadKeys } from 'src/app/shared/jwt/jwt-payload-keys';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationFacadeService {

  constructor(private authenticationService: AuthenticationService, private appStateService : AppStateService, 
    private jwtService : JwtService) { }

  public login(username: string, password: string):Observable<boolean> {
    const request: ILoginRequest = {username,password};
    
    return this.authenticationService.login(request).pipe(
      map((loginResponse: ILoginResponse) =>{
        this.appStateService.setAccessToken(loginResponse.accessToken);
        this.appStateService.setRefreshToken(loginResponse.refreshToken);

        const payload = this.jwtService.parsePayload(loginResponse.accessToken);
        this.appStateService.setUsername(payload[JwtPayloadKeys.Username]);
        this.appStateService.setEmail(payload[JwtPayloadKeys.Email]);
        this.appStateService.setRoles(payload[JwtPayloadKeys.Role]); 

        return true;
      }),
      catchError((err)=>{
        console.log(err);
        return of(false);
      })
    );
  }
}
