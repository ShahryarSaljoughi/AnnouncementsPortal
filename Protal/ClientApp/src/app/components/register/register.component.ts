import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { RegisterDto } from 'src/app/models/registerDto';
import { dashCaseToCamelCase } from '@angular/animations/browser/src/util';
import { HttpClient } from '@angular/common/http';
import {  AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  encapsulation: ViewEncapsulation.Emulated
})
export class RegisterComponent implements OnInit {
  dto: RegisterDto = new RegisterDto();
  confirmPassword = '';
  constructor(private http: HttpClient, private auth: AuthService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  checkPasswordMatch(): boolean {
    if (this.dto.password === this.confirmPassword) {
      return true;
    }
    return false;
  }

  register() {
    this.auth.register(this.dto.email, this.dto.password).subscribe((v) => {
      this.alertify.warning('ایمیل فعالسازی برای شما ارسال شد');
    this.router.navigate(['login']);
    },
    errr => {
      console.log(errr.error);
      this.alertify.error(errr.error);
    });
  }

}
