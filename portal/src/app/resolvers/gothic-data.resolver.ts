import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Stream } from "../models/stream";
import { StreamsService } from "../services/streams.service";

@Injectable({
  providedIn: 'root'
})
export class GothicDataResolver implements Resolve<{stream: Stream, gothicStream: Stream, ongoingGothic: boolean}> {
  constructor(private streams: StreamsService) { };

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.streams.getGothicData();
  }
}
