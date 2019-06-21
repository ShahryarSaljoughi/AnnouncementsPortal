import { Component, OnInit, ViewChild } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { NewAnnouncementDto } from 'src/app/models/newAnnouncementDto';
import { UploadService } from 'src/app/services/upload.service';

@Component({
  selector: 'app-create-announcement',
  templateUrl: './create-announcement.component.html',
  styleUrls: ['./create-announcement.component.css']
})
export class CreateAnnouncementComponent implements OnInit {
  public dto = new NewAnnouncementDto();
  uploadProgress: number = null;
  serverAssignedFileId: string;
  @ViewChild('file') file: any;

  constructor(private http: HttpClient, private uploadService: UploadService) { }

  ngOnInit() {
  }

  createPost() {
    debugger;
    this.dto.imageFileId = this.serverAssignedFileId;
    this.http.post('http://localhost:5000/api/Announcement/PostNewAnnouncement', this.dto).subscribe();
  }

  uploadFile() {
  }

  addFile() {
    this.file.nativeElement.click();
  }
  onFileAdded() {
    debugger;
    let selectedFile = this.file.nativeElement.files.item(this.file.nativeElement.files.length - 1);
    const formData = new FormData();
    formData.append('uploadedFile', selectedFile);
    this.uploadProgress = 0;
    this.uploadService.upload(formData).subscribe(
      (res) => {
        if (res.status === 'progress') {
          console.log(res.message);
          this.uploadProgress = res.message;
        } else if (res.status === 'response') {
          this.serverAssignedFileId = res.message.id;
        }
      },
      (err) => {
        console.log(err);
      }
    );
  }

  isUploading () {
    return !!this.uploadProgress;
  }

  isBusy(): boolean {
    const isBusy = !!this.uploadProgress && this.uploadProgress < 100;
    return isBusy;
  }

  getUploadProgress() {
    return `${this.uploadProgress}%`;
  }

}
