import { Injectable } from '@angular/core';
import { IJwtPayload } from './jwt-payload';

@Injectable({
  providedIn: 'root'
})
export class JwtService {

  constructor() { }

  public parsePayload(jwtString : string) : IJwtPayload{
    const jwtStringParts : string[] = jwtString.split(".");
    const payloadString = jwtStringParts[1];
    return JSON.parse(atob(payloadString)) as IJwtPayload;
    // atob vraca base64 dekodirani string (sto je json objekat kad se dekodira jwt payload)
    // as IJwtPayload eksplicitno kastujemo u taj tip, korisno je ako hocemo nakon parsiranja da izdvojimo neke delove
    // intellisense pomaze kad iskoristis to
  }
}
