import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { NewAnnouncementDto } from 'src/app/models/newAnnouncementDto';

@Component({
  selector: 'app-create-announcement',
  templateUrl: './create-announcement.component.html',
  styleUrls: ['./create-announcement.component.css']
})
export class CreateAnnouncementComponent implements OnInit {
  public dto = new NewAnnouncementDto();
  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  createPost() {
    this.http.post('http://localhost:5000/api/Announcement/PostNewAnnouncement', this.dto).subscribe();
  }

}
