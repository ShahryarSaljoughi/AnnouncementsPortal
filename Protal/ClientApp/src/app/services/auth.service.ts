import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginDto } from '../models/login-dto';
import { map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RegisterDto } from '../models/registerDto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<void> {
    let dto = new LoginDto(email, password);
    return this.http.post('http://localhost:5000/api/authentication/Login', dto).pipe(
      tap((response: any) => {
        localStorage.setItem('jwtToken', response.jwt);
        // return response.jwt;
      }));
  }

  register(email: string, password: string) {
    let dto = new RegisterDto(email, password);
    return this.http.post('http://localhost:5000/api/Authentication/Register', dto);
  }


}
