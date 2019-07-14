import { Component, OnInit } from '@angular/core';
import { TeacherService } from 'src/app/services/teacher.service';
import { TeacherDto } from 'src/app/models/teacherDto';
import { UpdateProfileDto } from 'src/app/models/UpdateProfileDto';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  teacherInfo: TeacherDto = new TeacherDto();
  teacherNewInfo: UpdateProfileDto = new UpdateProfileDto();
  constructor(private teacherService: TeacherService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.teacherService.getTeacherInfo().subscribe((value) => {
      this.teacherInfo = value;
      this.teacherNewInfo.phone = value.phone;
      this.teacherNewInfo.znuUrl = value.znuUrl;
      this.teacherNewInfo.firstName = value.firstName || value.name;
      this.teacherNewInfo.lastName = value.lastName;
      console.log(this.teacherInfo);
    },
    err => {
      console.log('something bad happened while requesting teacher info');
    });
  }

  updateProfile() {
    this.teacherService.updateTeacherProfile(this.teacherNewInfo)
      .subscribe(
        (val) => this.alertify.success('تغییرات ذخیره شدند'),
        err => this.alertify.error(err.error));
  }


}
