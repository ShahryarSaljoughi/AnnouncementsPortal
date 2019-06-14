import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UploadService {

  constructor(private httpClient: HttpClient) {   }

  upload(data): Observable<{status: any, message: any}> {

    const uploadURL = 'http://localhost:5000/api/Announcement/UploadFile';

    return this.httpClient.post<any>(uploadURL, data, {
      reportProgress: true,
      observe: 'events'
    }).pipe(map((event) => {
      switch (event.type) {
        case HttpEventType.UploadProgress:
          const progress = Math.round(100 * event.loaded / event.total);
          return { status: 'progress', message: progress };
        case HttpEventType.Response:
          return {status: 'response', message: event.body};
        default:
          return {status: 'Unhandled event', message: `${event.type}` } ;
      }
    })
    );
  }
}
