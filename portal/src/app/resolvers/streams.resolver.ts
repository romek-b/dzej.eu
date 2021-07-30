import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { iif, of } from "rxjs";
import { map, switchMap } from 'rxjs/operators';
import { Stream } from "../models/stream";
import { StreamsService } from "../services/streams.service";

@Injectable({
  providedIn: 'root'
})
export class StreamsResolver implements Resolve<{latestStream: Stream, selectedStream: Stream}> {
  constructor(private streams: StreamsService) { };

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const gameName = route.data.gameName;
    return this.streams.getLatest().pipe(
      switchMap(latestStream => iif(
        () => gameName && !this.gamePattern(gameName).test(latestStream.gameName),
        this.streams.getLatestByName(gameName).pipe(
          map(selectedStream => ({latestStream, selectedStream}))
        ),
        of({latestStream, selectedStream: latestStream}),
      ))
    )
  }

  private gamePattern(gameName: string) {
    return new RegExp(`^.*${gameName}.*$`,'i');
  }
}
