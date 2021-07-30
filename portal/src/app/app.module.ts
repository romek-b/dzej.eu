import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { StreamsComponent } from './streams/streams.component';
import { HttpClientModule } from '@angular/common/http';
import { WheelOfFortuneComponent } from './wheel-of-fortune/wheel-of-fortune.component';
import { NgxWheelModule } from 'ngx-wheel';

@NgModule({
  declarations: [
    AppComponent,
    StreamsComponent,
    WheelOfFortuneComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgxWheelModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
