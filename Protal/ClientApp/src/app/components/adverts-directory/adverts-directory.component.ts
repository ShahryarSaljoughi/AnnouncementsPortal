import { Component, OnInit } from '@angular/core';
import { Announcement } from 'src/app/models/Announcement';
import { AdvertService } from 'src/app/services/advert.service';

@Component({
  selector: 'app-adverts-directory',
  templateUrl: './adverts-directory.component.html',
  styleUrls: ['./adverts-directory.component.css']
})
export class AdvertsDirectoryComponent implements OnInit {
  private announcements: Announcement[] = new Array<Announcement>();
  constructor(private adService: AdvertService) { }

  ngOnInit() {
    this.FillAnnouncements();
    debugger;
  }

  FillAnnouncements() {
    debugger;
    this.adService.getAdverts(10, 1).subscribe((val) => {
      val.forEach(element => {
        this.announcements.push(element);
      });
    });
    debugger;
  }

}
