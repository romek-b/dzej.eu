import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { Subscription, interval } from 'rxjs';
import { Stream } from '../models/stream';
import * as moment from 'moment';
import 'moment-duration-format';

@Component({
  selector: 'app-gothic',
  templateUrl: 'gothic.component.html',
  styleUrls: ['gothic.component.css']
})
export class GothicComponent implements OnInit {
  timerSub: Subscription;

  newestStream: Stream;
  newestGothicStream: Stream;

  ongoing: boolean;
  ongoingGothic: boolean;

  gothicTimestamp = "dawno";

  constructor(private route: ActivatedRoute, private titleService: Title) { }

  ngOnInit(): void {
    moment.locale('pl');
    this.titleService.setTitle("Gothic - dzej.eu");

    this.newestStream = this.route.snapshot.data.gothicData.stream;
    this.newestGothicStream = this.route.snapshot.data.gothicData.gothicStream;
    this.ongoing = this.newestStream?.isOngoing;
    this.ongoingGothic = this.route.snapshot.data.gothicData.ongoingGothic;
    this.setGothicTimeStamp();

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
