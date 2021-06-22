import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GothicComponent } from './gothic/gothic.component';
import { GothicDataResolver } from './resolvers/gothic-data.resolver';
import { TopicsResolver } from './resolvers/topics.resolver';
import { WheelOfFortuneComponent } from './wheel-of-fortune/wheel-of-fortune.component';

const routes: Routes = [
  { path: 'gothic', component: GothicComponent, resolve: { gothicData : GothicDataResolver }},
  { path: 'losowanie', component: WheelOfFortuneComponent, resolve: { topics: TopicsResolver }},
  { path: '', redirectTo: '/gothic', pathMatch: 'prefix'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }