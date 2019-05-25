import { Component, OnInit } from '@angular/core';
import { Announcement } from 'src/app/models/Announcement';

@Component({
  selector: 'app-adverts-directory',
  templateUrl: './adverts-directory.component.html',
  styleUrls: ['./adverts-directory.component.css']
})
export class AdvertsDirectoryComponent implements OnInit {
  private announcements: Announcement[];
  constructor() { }

  ngOnInit() {
    this.announcements = this.getAnnouncements();
  }

  getAnnouncements() {
    let a = new Announcement();
    a.authorFirstName = 'علی';
    a.text = 'این متن تستی اعلانیه است این متن تستی اعلانیه است این متن تستی اعلانیه است این متن تستی اعلانیه است این متن تستی اعلانیه است این متن تستی اعلانیه است این متن تستی اعلانیه است این متن تستی اعلانیه است این متن تستی اعلانیه است ';
    a.title = 'این عنوان ماک اعلانیه است';

    let b = new Announcement();
    b.authorFirstName = 'علی';
    b.text = 'این متن تستی اعلانیه است';
    b.title = 'این عنوان ماک اعلانیه است';
    const result: Announcement[] = [
      a, b
    ];
    return result;
  }

}
