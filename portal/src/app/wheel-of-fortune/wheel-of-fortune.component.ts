import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxWheelComponent } from 'ngx-wheel';
import { TopicsService } from '../services/topics.service';

@Component({
  selector: 'app-wheel-of-fortune',
  templateUrl: 'wheel-of-fortune.component.html',
  styleUrls: ['wheel-of-fortune.component.css']
})
export class WheelOfFortuneComponent implements OnInit {
  @ViewChild(NgxWheelComponent) wheel: any;
  private colors = ['#FF0000', '#0000FF']

  topics: any[];
  result: number;
  winner: string;

  wheelSize: number;

  started: boolean;
  completed: boolean;

  spinSound: HTMLAudioElement;
  constructor(private service: TopicsService) { }

  ngOnInit(): void {
    this.resizeWheel();
    this.service.getAll().subscribe(res => {
      this.topics = res.map((value, index) => ({
        fillStyle: this.colors[index % 2],
        text: value,
        id: index,
        textFillStyle: 'white',
        textFontSize: '13'
      }));
      this.reset();
    });

    this.spinSound = new Audio("./assets/zakrecmy_koem_fortuny.mp3");
    this.spinSound.load();
  }

  resizeWheel(): void {
    this.wheelSize = Math.floor(Math.min(window.innerHeight, window.innerWidth) * 0.8);
  }

  spinStart(): void {
    this.spinSound.play();
    this.started = true;
  }

  spinComplete(): void {
    this.completed = true;
    this.winner = this.topics.find(x => x.id == this.result).text;
  }

  reset(): void {
    this.started = false;
    this.completed = false;
    this.result = Math.floor(Math.random() * this.topics.length);
    this.wheel?.reset();
  }
}
