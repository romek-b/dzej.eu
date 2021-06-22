import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { TopicsService } from "../services/topics.service";

@Injectable({
  providedIn: 'root'
})
export class TopicsResolver implements Resolve<string[]> {
  constructor(private topics: TopicsService) { };

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.topics.getAll();
  }
}
