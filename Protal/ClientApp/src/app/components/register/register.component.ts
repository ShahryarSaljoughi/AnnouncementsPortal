import { Component, OnInit } from '@angular/core';
import { RegisterDto } from 'src/app/models/registerDto';
import { dashCaseToCamelCase } from '@angular/animations/browser/src/util';
import { HttpClient } from '@angular/common/http';
import {  AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  dto: RegisterDto = new RegisterDto();
  confirmPassword = '';
  constructor(private http: HttpClient, private auth: AuthService, private router: Router) { }

  ngOnInit() {
  }

  checkPasswordMatch(): boolean {
    if (this.dto.password === this.confirmPassword) {
      return true;
    }
    return false;
  }

  register() {
    this.auth.register(this.dto.email, this.dto.password).subscribe((v) => {});
    this.router.navigate(['login']);
  }

}
