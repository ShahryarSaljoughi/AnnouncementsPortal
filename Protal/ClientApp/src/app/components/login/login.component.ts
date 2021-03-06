import { Observable} from 'rxjs';
import { map } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { LoginDto } from '../../models/login-dto';
import { HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public dto: LoginDto = new LoginDto();
  private key : string;
  constructor(private httpClient: HttpClient, private auth: AuthService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    this.auth.login(this.dto.email, this.dto.password).subscribe(
      value => {
        this.router.navigate(['/directory']);
      }
      , err => {
        this.alertify.error(err.error);
        console.log(err);
      });
    
  }
}


