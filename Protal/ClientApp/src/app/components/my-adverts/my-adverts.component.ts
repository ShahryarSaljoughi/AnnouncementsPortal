import { Component, OnInit } from '@angular/core';
import { TeacherService } from 'src/app/services/teacher.service';
import { Announcement } from 'src/app/models/Announcement';
import { AdvertService } from 'src/app/services/advert.service';
import { ThreeDotPipe } from 'src/app/helper/dotdotdot';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-my-adverts',
  templateUrl: './my-adverts.component.html',
  styleUrls: ['./my-adverts.component.css']
})
export class MyAdvertsComponent implements OnInit {
  teacherId: string;
  myAdverts: Announcement[] = new Array<Announcement>();
  constructor(private teacherService: TeacherService, private advertService: AdvertService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.setTeacherId();
    this.getMyAdverts();
    console.log(this.myAdverts);
  }
  getMyAdverts() {
    this.advertService.getMyAdverts(30, 1).subscribe(
      (val) => {
        val.forEach(element => {
          this.myAdverts.push(element);
        });
      }
    );
  }
  setTeacherId() {
    this.teacherId = localStorage.getItem('teacherId');
    if (!this.teacherId) {
      this.teacherService.getTeacherInfo().subscribe(
        (val) => {
          localStorage.setItem('teacherId', val.teacherId);
          this.teacherId = val.teacherId;
        }
      )
    }
  }

  removeAdvert(adId: string) {
    this.advertService.removeAdvert(adId).subscribe(
      (val) => {
        const itemToRemoveIndex = this.myAdverts.findIndex(
          (value, index, obj) => {
            if (value.announcementId === adId) {
              return true;
            }
            return false;
          }
        );
        if (itemToRemoveIndex > -1) {
          this.myAdverts.splice(itemToRemoveIndex, 1);
        }
      }

      (err) => {
        this.alertify.error(err.error);
      }
    );
  }

}
