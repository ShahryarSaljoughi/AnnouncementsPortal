import { Component, OnInit, Input } from '@angular/core';
import { Announcement } from 'src/app/models/Announcement';
import { ThreeDotPipe } from 'src/app/helper/dotdotdot';
@Component({
  selector: 'app-adver-card',
  templateUrl: './adver-card.component.html',
  styleUrls: ['./adver-card.component.css']
})
export class AdverCardComponent implements OnInit {
  private advertText: string;
  private advertTextCharCountLimit = 200;
  constructor() { }
  @Input() advertData: Announcement;
  ngOnInit() {
    this.setAdvertText();
  }

  setAdvertText() {
    if (this.advertData.text.length < this.advertTextCharCountLimit) {
      this.advertText = this.advertData.text;
    } else {
      this.advertText = this.advertData.text.slice(0, this.advertTextCharCountLimit) + ' ...';
    }
  }

  showFullAdvertText() {
    this.advertText = this.advertData.text;
  }

  shouldShowSeeMoreOption(): boolean {
    return this.advertData.text.length > this.advertTextCharCountLimit && this.advertText !== this.advertData.text;
  }



}
