import { Component, OnInit } from '@angular/core';
import { RegisterDto } from 'src/app/models/registerDto';
import { dashCaseToCamelCase } from '@angular/animations/browser/src/util';
import { HttpClient } from '@angular/common/http';
import {  AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  dto: RegisterDto = new RegisterDto();
  confirmPassword = '';
  constructor(private http: HttpClient, private auth: AuthService) { }

  ngOnInit() {
  }

  checkPasswordMatch(): boolean {
    if (this.dto.password === this.confirmPassword) {
      return true;
    }
    return false;
  }

  register() {
    debugger;
    this.auth.register(this.dto.email, this.dto.password).subscribe((v) => {});
  }

}
