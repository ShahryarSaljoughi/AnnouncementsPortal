import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
}
