import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { iif, of, Subscription, interval } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { Stream } from '../models/Stream';
import { StreamsService } from '../services/streams.service';
import * as moment from 'moment';
import 'moment-duration-format';

@Component({
  selector: 'app-gothic',
  templateUrl: 'gothic.component.html'
})

export class GothicComponent implements OnInit {
  timerSub: Subscription;

  newestStream: Stream;
  newestGothicStream: Stream;

  ongoing: boolean;
  ongoingGothic: boolean;

  gothic = 'gothic'; // duh
  gothicPattern = /^.*gothic.*$/i;

  gothicTimestamp = "dawno";

  constructor(private streams: StreamsService, private titleService: Title) {
  }

  ngOnInit(): void {
    moment.locale('pl');
    this.titleService.setTitle("Gothic - dzej.eu");

    this.streams.getNewest().pipe(
      switchMap(stream => of({
          stream: stream,
          ongoingGothic: this.gothicPattern.test(stream.gameName) && stream.isOngoing
        }).pipe(
          switchMap(({ongoingGothic}) => iif(
            () => !ongoingGothic,
            this.streams.getNewestByName(this.gothic).pipe(
              map(gothicStream => ({stream, gothicStream, ongoingGothic}))
            ),
            of({stream, gothicStream: stream, ongoingGothic})
          ))
        )
      )
    ).subscribe(({stream, gothicStream, ongoingGothic}) => {
      this.newestStream = stream;
      this.newestGothicStream = gothicStream;
      this.ongoing = stream.isOngoing;
      this.ongoingGothic = ongoingGothic;
      this.setGothicTimeStamp();
    });

    interval(1000).pipe().subscribe(() => {
      this.setGothicTimeStamp();
    })
  }

  setGothicTimeStamp() {
    if(this.newestGothicStream) {
      this.gothicTimestamp = moment
        .duration(moment(Date.now()).diff(this.newestGothicStream.startedAt))
        .format("y[y] M[M] d[d] h[h] m[m] s[s]");
    }
  }
}
