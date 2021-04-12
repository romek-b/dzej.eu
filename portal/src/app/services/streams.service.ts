import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Stream } from '../models/stream';

@Injectable({
  providedIn: 'root'
})
export class StreamsService {

  constructor(private http: HttpClient) { }

  getNewest() {
    return this.http.get<Stream>(`/api/streams/dzejth`);
  }

  getNewestByName(name: string) {
    return this.http.get<Stream>(`/api/streams/dzejth/${name}`);
  }
}
