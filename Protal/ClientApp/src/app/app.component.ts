import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  constructor(private route: ActivatedRoute, private router: Router, private location: Location) {
  }
  isHomePage(): boolean {
    const path = this.location.path();
    const result = path === '/' || path === '';
    return result;
  }
}
