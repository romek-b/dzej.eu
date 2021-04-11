import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GothicComponent } from './gothic/gothic.component';
import { WheelOfFortuneComponent } from './wheel-of-fortune/wheel-of-fortune.component';

const routes: Routes = [
  { path: 'gothic', component: GothicComponent },
  { path: 'losowanie', component: WheelOfFortuneComponent },
  { path: '', redirectTo: '/gothic', pathMatch: 'prefix'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }