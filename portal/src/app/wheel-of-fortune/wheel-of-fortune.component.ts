import { Component, OnInit, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { NgxWheelComponent } from 'ngx-wheel';

@Component({
  selector: 'app-wheel-of-fortune',
  templateUrl: 'wheel-of-fortune.component.html',
  styleUrls: ['wheel-of-fortune.component.css']
})
export class WheelOfFortuneComponent implements OnInit {
  @ViewChild(NgxWheelComponent) wheel: any;
  private colors = ['#FF0000', '#0000FF']

  private dropTimestamp = 8.78; // when music drops in the video

  topics: any[];
  result: number;
  winner: string;

  wheelSize: number;

  videoStarted: boolean;
  spinStarted: boolean;
  completed: boolean;

  constructor(private route: ActivatedRoute, private titleService: Title) { }

  ngOnInit(): void {
    this.titleService.setTitle("Losowanie - dzej.eu");
    this.resizeWheel();
    this.topics = this.route.snapshot.data.topics.map((value, index) => ({
      fillStyle: this.colors[index % 2],
      text: value,
      id: index,
      textFillStyle: 'white',
      textFontSize: this.wheelSize/37
    }));
    this.reset();
  }

  resizeWheel(): void {
    this.wheelSize = Math.floor(Math.min(window.innerHeight, window.innerWidth) * 0.8);
  }

  videoStart(): void {
    this.videoStarted = true;
  }

  spinComplete(): void {
    this.completed = true;
    this.videoStarted = false;
    this.winner = this.topics.find(x => x.id == this.result).text;
  }

  reset(): void {
    this.spinStarted = false;
    this.completed = false;
    this.result = Math.floor(Math.random() * this.topics.length);
    this.wheel?.reset();
  }

  log(event: any) {
    if(!this.spinStarted && event.target.currentTime > this.dropTimestamp) {
      this.wheel.spin();
    };
  }
}
