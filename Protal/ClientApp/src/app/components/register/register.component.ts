import { Component, OnInit } from '@angular/core';
import { RegisterDto } from 'src/app/models/registerDto';
import { dashCaseToCamelCase } from '@angular/animations/browser/src/util';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  dto: RegisterDto = new RegisterDto();
  confirmPassword = '';
  constructor(private http: HttpClient) { }

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
    this.http.post('http://localhost:5000/api/Authentication/Register', this.dto).subscribe((v) => {});
  }

}
