import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { Observable, timer } from 'rxjs';
import { map } from 'rxjs/operators';
import { Stream } from '../models/stream';
import * as moment from 'moment';
import 'moment-duration-format';

@Component({
  selector: 'app-streams',
  templateUrl: 'streams.component.html',
  styleUrls: ['streams.component.css']
})
export class StreamsComponent implements OnInit {
  timestamp$: Observable<string>;

  latestStream: Stream;
  latestSelectedStream: Stream;

  selectedGameName?: string;

  constructor(private route: ActivatedRoute, private titleService: Title) { }

  ngOnInit(): void {
    moment.locale('pl');

    this.selectedGameName = this.route.snapshot.data.gameName;

    if(this.selectedGameName) {
      this.titleService.setTitle(`${this.selectedGameName} - dzej.eu`);
    }

    this.latestStream = this.route.snapshot.data.streams.latestStream;
    this.latestSelectedStream = this.route.snapshot.data.streams.selectedStream;

    this.timestamp$ = timer(0, 1000).pipe(
      map(() => this.getTimeStamp())
    );
  }

  getTimeStamp() {
    if(this.latestSelectedStream) {
      return moment
        .duration(moment(Date.now()).diff(this.latestSelectedStream.startedAt))
        .format("y[y] M[M] d[d] h[h] m[m] s[s]");
    } else {
      return "dawno"; // lul
    }
  }
}
