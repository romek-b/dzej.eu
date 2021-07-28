import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class ExternalUrlResolver implements Resolve<any> {
  constructor() {
      console.log('elo');
  }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    window.location.href = route.data.url;
  }
}