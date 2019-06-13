import { Injectable } from '@angular/core';
declare let alertify: any; // alertify is installed and imported in angular.json globally! this is just to shut toe lint down!


@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, function(e){
        if(e) {
            okCallback();
        } else {}
    });
}

success(message: string){
    alertify.success(message);
}

warning(message: string) {
    alertify.warning(message);
}


message(message: string) {
    alertify.message(message);
}


error(message: string) {
    alertify.error(message);
}
}

