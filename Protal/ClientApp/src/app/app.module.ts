import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {JwtModule} from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CreateAnnouncementComponent } from './components/create-announcement/create-announcement.component';
import { AdverCardComponent } from './components/adver-card/adver-card.component';
import { AdvertsDirectoryComponent } from './components/adverts-directory/adverts-directory.component';
import { ThreeDotPipe } from './helper/dotdotdot';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegisterComponent,
    CreateAnnouncementComponent,
    AdverCardComponent,
    AdvertsDirectoryComponent,
    ThreeDotPipe,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
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
      { path: 'newAnnouncement', component: CreateAnnouncementComponent},
      
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function GetToken() {
  return localStorage.getItem('jwtToken');
}
