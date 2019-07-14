import { Component, OnInit } from '@angular/core';
import { Announcement } from 'src/app/models/Announcement';
import { AdvertService } from 'src/app/services/advert.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-adverts-directory',
  templateUrl: './adverts-directory.component.html',
  styleUrls: ['./adverts-directory.component.css']
})
export class AdvertsDirectoryComponent implements OnInit {
  private announcements: Announcement[] = new Array<Announcement>();
  private adsNo: number = 0;
  private pageSize: number = 10;
  private pageNumber: number = 1;
  constructor(private adService: AdvertService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.FillAnnouncements();
    this.getAdsNo();
  }
  getAdsNo() {
    this.adService.getAdsNo().subscribe(
      val => {
        this.adsNo = val;
        console.log(`number of fetched ads: ${val} `);
      },
      err => {
        this.alertify.error('مشکلی در دریافت آگهی ها پیش آمد');
      }
    );
  }

  FillAnnouncements() {
    this.adService.getAdverts(this.pageSize, this.pageNumber).subscribe((val) => {
      val.forEach(element => {
        this.announcements.push(element);
      });
    });
  }

  loadMore() {
    this.pageNumber ++;
    this.FillAnnouncements();
  }

}
