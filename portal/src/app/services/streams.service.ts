import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Stream } from '../models/stream';
import { map, switchMap } from 'rxjs/operators';
import { iif, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StreamsService {
  gothic = 'gothic'; // duh
  gothicPattern = /^.*gothic.*$/i;

  constructor(private http: HttpClient) { }

  getGothicData() {
    return this.getNewest().pipe(
      switchMap(stream => of({
          stream: stream,
          ongoingGothic: stream?.isOngoing && this.gothicPattern.test(stream.gameName)
        }).pipe(
          switchMap(({ongoingGothic}) => iif(
            () => !ongoingGothic,
            this.getNewestByName(this.gothic).pipe(
              map(gothicStream => ({stream, gothicStream, ongoingGothic}))
            ),
            of({stream, gothicStream: stream, ongoingGothic})
          ))
        )
      )
    )
  }

  getNewest() {
    return this.http.get<Stream>(`/api/streams/dzejth`);
  }

  getNewestByName(name: string) {
    return this.http.get<Stream>(`/api/streams/dzejth/${name}`);
  }
}
