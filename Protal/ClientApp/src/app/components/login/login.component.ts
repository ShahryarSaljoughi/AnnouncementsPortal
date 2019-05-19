import { Observable} from 'rxjs';
import { map } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { LoginDto } from '../../models/login-dto';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public dto: LoginDto = new LoginDto();
  private key : string;
  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
  }

  login() {
    debugger;
    this.httpClient.post<string>('http://localhost:5000/api/authentication/Login', this.dto)
    .pipe(
      map((response: any) => {
        debugger;
        localStorage.setItem('jwtToken', response.jwt);
      })).subscribe();
  }
}


