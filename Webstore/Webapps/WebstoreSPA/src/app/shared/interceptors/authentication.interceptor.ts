import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, switchMap, take } from 'rxjs';
import { AppStateService } from '../app-state/app-state.service';
import { IAppState } from '../app-state/app-state';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  private readonly whilelistUrls : string[] = [
    '/api/v1/Authentication/Login'
  ];

  constructor(private appStateService: AppStateService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    if(this.isWhitelisted(request.url)){
        return next.handle(request);
    }

    return this.appStateService.getAppState().pipe(
      take(1),
      switchMap( (appState: IAppState) => {
          if (appState.accessToken !== undefined){
            request = this.addToken(request,appState.accessToken);
          }

          return next.handle(request)
      })
    )
  }

  private isWhitelisted(url: string) : boolean{
    return this.whilelistUrls.some((whilelistedUrl: string) => url.includes(whilelistedUrl));  
    
  }

  private addToken(request:HttpRequest<unknown>, accessToken: string) : HttpRequest<unknown>{


    return request.clone({
      setHeaders :{
        Authorization : `Bearer ${accessToken}`
      }
    })
  }

}

// Intercepts a http request before it was sent, and passes it to the next HttpHandler 
// (might be an another interceptor or the Http request sending)

// @Injectable - we think of this as one service - 
    // by default it has no providedIn : 'root' (which means it's singleton)
    // Angular wont create this one as a singleton
    // this service needs to be imported somewhere in order to get compiled
    // I imported it in app-module.ts -> providers 
    // [{ provide: HTTP_INTERCEPTORS, useClass : AuthenticationInterceptor, multi : true}]
    // this says: register as an interceptor the class AuthenticationInterceptor, and multi:true says i can 
    // register multiple interceptors