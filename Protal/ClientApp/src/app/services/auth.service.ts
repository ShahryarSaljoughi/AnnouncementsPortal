import { JwtHelperService, JwtModule, JwtInterceptor } from '@auth0/angular-jwt';

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginDto } from '../models/login-dto';
import { map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RegisterDto } from '../models/registerDto';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private router: Router) { }

  login(email: string, password: string): Observable<void> {
    let dto = new LoginDto(email, password);
    return this.http.post('http://localhost:5000/api/authentication/Login', dto).pipe(
      tap((response: any) => {
        // remove if any expired token is still there
        localStorage.removeItem('jwtToken'); 
        localStorage.setItem('jwtToken', response.jwt);
      }));
  }

  register(email: string, password: string) {
    let dto = new RegisterDto(email, password);
    return this.http.post('http://localhost:5000/api/Authentication/Register', dto);
  }

  isLoggedIn() {
    debugger;
    let token = localStorage.getItem('jwtToken');
    let jwtHelper = new JwtHelperService();
    if (token == null) {return false;}
    if (jwtHelper.isTokenExpired(token)) {return false; }
    return true;
  }

  signOut() {
    localStorage.removeItem('jwtToken');
    this.router.navigate(['']);
  }


}
