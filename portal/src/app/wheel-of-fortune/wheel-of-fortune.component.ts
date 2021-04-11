import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-wheel-of-fortune',
  templateUrl: 'wheel-of-fortune.component.html',
  styleUrls: ['wheel-of-fortune.component.css']
})
export class WheelOfFortuneComponent implements OnInit {

  public items: any[];
  seed = [
    { id: 0, text: 'Gothic' },
    { id: 1, text: 'Dark Souls 3' },
    { id: 2, text: 'sprzątamy' },
    { id: 3, text: 'zostaje czy oddaje' },
    { id: 4, text: 'brak koncepcji' },
    { id: 5, text: 'planszowka'},
    { id: 6, text: 'Splitter działa' },
    { id: 7, text: 'NO SIGNAL' },
    { id: 8, text: 'MGS3' },
    { id: 9, text: 'NIE MA SPANIA, SĄSIADKA' },
    { id: 10, text: 'ŚWIECA' },
    { id: 11, text: 'mam jutro na 5 do pracy' }
  ]
  colors = ['#FF0000', '#0000FF']
  idToLandOn: any;
  started = false;
  completed = false;

  spinSound: HTMLAudioElement;
  constructor() { }

  ngOnInit(): void {
    this.idToLandOn = this.seed[Math.floor(Math.random() * this.seed.length)].id;
    this.items = this.seed.map((value) => ({
      fillStyle: this.colors[value.id % 2],
      text: value.text,
      id: value.id,
      textFillStyle: 'white',
      textFontSize: '16'
    }));

    this.spinSound = new Audio("./assets/zakrecmy_koem_fortuny.mp3");
    this.spinSound.load();
  }

  spinStart(): void {
    this.spinSound.play();
    this.started = true;
  }

  spinComplete(): void {
    this.completed = true;
  }
}
