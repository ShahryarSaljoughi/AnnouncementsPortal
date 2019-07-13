import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(public auth: AuthService, private router: Router) {  }
  collapse() {
    this.isExpanded = false;
  }

  signOut() {
    this.auth.signOut();
  }

  isAuthenticated(): boolean {
    return this.auth.isLoggedIn();
  }

  goToNewPost() {
    this.router.navigate(['newAnnouncement']);
  }

  goToProfile() {
    this.router.navigate(['profile']);
  }

  goToMyAdverts() {
    this.router.navigate(['myAdverts']);
  }

  toggle() {
    this.isExpanded = ! this.isExpanded;
  }
}
