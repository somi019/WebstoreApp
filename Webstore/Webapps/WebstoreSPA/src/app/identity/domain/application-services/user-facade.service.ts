import { Injectable } from '@angular/core';
import { UserService } from '../infrastructure/user.service';
import { Observable } from 'rxjs';
import { IUserDetails } from '../models/user-details';

@Injectable({
  providedIn: 'root'
})
export class UserFacadeService {

  constructor(private userService : UserService) { }

  public getUserDetails(username : string) : Observable<IUserDetails>{
    return this.userService.getUserDetails(username);
  }
  
}

// fasada je aplikacioni sloj, ovde nista ne radi u odnosu na UserService u infrastrukturi, ali bolje da se razdvoji
// ako posle dodajemo nesto u aplikacionom delu, infra samo poziva api
