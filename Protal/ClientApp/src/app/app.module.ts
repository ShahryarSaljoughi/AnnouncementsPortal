import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {JwtModule} from '@auth0/angular-jwt';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';

import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CreateAnnouncementComponent } from './components/create-announcement/create-announcement.component';
import { AdverCardComponent } from './components/adver-card/adver-card.component';
import { AdvertsDirectoryComponent } from './components/adverts-directory/adverts-directory.component';
import { ThreeDotPipe } from './helper/dotdotdot';
import { AdverDetailComponent } from './components/adver-detail/adver-detail.component';
import { AuthGuard } from './auth.guard';
import { LandingComponent } from './components/landing/landing.component';
import { HomeComponent } from './components/home/home.component';
import { ProfileComponent } from './components/profile/profile.component';
import { MyAdvertsComponent } from './components/my-adverts/my-adverts.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegisterComponent,
    CreateAnnouncementComponent,
    AdverCardComponent,
    AdvertsDirectoryComponent,
    ThreeDotPipe,
    AdverDetailComponent,
    LandingComponent,
    ProfileComponent,
    MyAdvertsComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    NgbModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: GetToken,
        whitelistedDomains: ['localhost:5000']
      }
    }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'login', component: LoginComponent},
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'register', component: RegisterComponent},
      { path: 'directory', component: AdvertsDirectoryComponent},
      { path: 'newAnnouncement', component: CreateAnnouncementComponent, canActivate: [AuthGuard]},
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard]},
      { path: 'myAdverts', component: MyAdvertsComponent, canActivate: [AuthGuard]}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function GetToken() {
  return localStorage.getItem('jwtToken');
}
