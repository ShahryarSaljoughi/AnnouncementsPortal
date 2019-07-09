import { Component, OnInit, ViewChild } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { NewAnnouncementDto } from 'src/app/models/newAnnouncementDto';
import { UploadService } from 'src/app/services/upload.service';
import { AlertifyService } from 'src/app/services/alertify.service';
@Component({
  selector: 'app-create-announcement',
  templateUrl: './create-announcement.component.html',
  styleUrls: ['./create-announcement.component.css']
})
export class CreateAnnouncementComponent implements OnInit {
  public dto = new NewAnnouncementDto();
  uploadProgress: number = null;
  serverAssignedFileId: string;
  imgSrc: string | ArrayBuffer;
  @ViewChild('file') file: any = null;

  constructor(private http: HttpClient, private uploadService: UploadService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  createPost() {
    this.dto.imageFileId = this.serverAssignedFileId;
    this.http.post('http://localhost:5000/api/Announcement/PostNewAnnouncement', this.dto).subscribe();
    this.alertify.success("اعلانیه شما با موفقیت درج شد!");
  }

  uploadFile() {
  }

  addFile() {
    this.file.nativeElement.click();
  }
  onFileAdded() {
    this.previewFile();
    const selectedFile = this.file.nativeElement.files.item(this.file.nativeElement.files.length - 1);
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

  previewFile() {
    const the_file = this.file.nativeElement.files.item(0);
    if (the_file) {
      const reader = new FileReader();
      reader.onload = e => this.imgSrc = reader.result;
      reader.readAsDataURL(the_file);
    }
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

  isFileSelected(): boolean {
    debugger;
    if (this.file.nativeElement.files.length === 0) {
      return false;
    }
    return true;

  }

  removeSelectedFile() {
    this.dto.imageFileId = null;
    this.uploadProgress = null;
    console.log(`selected file is: ${this.file.nativeElement.value}`);
    this.file.nativeElement.value = '';
    this.previewFile()
  }

  getFilePath() {
    const result = this.file.nativeElement.value;
    return result;
  }

}
