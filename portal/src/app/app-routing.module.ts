import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { StreamsComponent } from './streams/streams.component';
import { ExternalUrlResolver } from './resolvers/external-url.resolver';
import { StreamsResolver } from './resolvers/streams.resolver';
import { TopicsResolver } from './resolvers/topics.resolver';
import { WheelOfFortuneComponent } from './wheel-of-fortune/wheel-of-fortune.component';

const routes: Routes = [
  { 
    path: 'losowanie', 
    component: WheelOfFortuneComponent, 
    resolve: { topics: TopicsResolver }
  },
  { 
    path: 'bercik', 
    component: AppComponent, 
    resolve: { url: ExternalUrlResolver }, 
    data: { url: 'https://consumer.huawei.com/'}
  },
  { 
    path: 'gothic',
    component: StreamsComponent,
    resolve: { streams : StreamsResolver },
    data: { gameName: 'Gothic' }
  },
  { 
    path: '',
    component: StreamsComponent,
    resolve: { streams : StreamsResolver }
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
