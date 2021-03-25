import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GothicComponent } from './gothic/gothic.component';

const routes: Routes = [
  { path: 'gothic', component: GothicComponent },
  { path: '', redirectTo: '/gothic', pathMatch: 'prefix'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }