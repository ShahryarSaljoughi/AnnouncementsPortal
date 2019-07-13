import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Announcement } from '../models/Announcement';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  constructor(private http: HttpClient) { }

  getAdverts(pageSize: number = 15, pageNumber: number): Observable<Announcement[]> {
    return this.http.post<Announcement[]>(
      'http://localhost:5000/api/Announcement/GetAnnouncements'
      , {
        pageSize: pageSize,
        pageNumber: pageNumber
    });
  }

  getMyAdverts(pageSize: number = 15, pageNumber: number) {
    return this.http.post<Announcement[]>(
      'http://localhost:5000/api/Announcement/GetAnnouncements'
      , {
        pageSize: pageSize,
        pageNumber: pageNumber,
        filter: {
          ownerId: localStorage.getItem('teacherId')
        }
    });
  }

  removeAdvert(advertId: string) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: {
        announcementId: advertId
      },
    };
    return this.http.delete(
      'http://localhost:5000/api/Announcement/DeleteAnnouncement'
      , options);
  }
}
