import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TeacherDto } from '../models/teacherDto';
import { Observable } from 'rxjs';
import { UpdateProfileDto } from '../models/UpdateProfileDto';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  constructor(private http: HttpClient) { }

  getTeacherInfo(): Observable<TeacherDto> {
    return this.http.get<TeacherDto>('http://localhost:5000/api/Teacher/GetTeacherInfo');
  }

  updateTeacherProfile(dto: UpdateProfileDto) {
    return this.http.post('http://localhost:5000/api/Teacher/UpdateProfile', dto);
  }
}
