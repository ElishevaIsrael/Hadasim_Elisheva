import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MembersListComponent } from './members-list/members-list.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    MembersListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,CommonModule
  ],
  providers: [
    provideClientHydration(),provideHttpClient(withFetch()), [HttpClient]
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

