import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Stream } from '../models/stream';

@Injectable({
  providedIn: 'root'
})
export class StreamsService {
  constructor(private http: HttpClient) { }

  getLatest() {
    return this.http.get<Stream>(`/api/streams/dzejth`);
  }

  getLatestByName(name: string) {
    return this.http.get<Stream>(`/api/streams/dzejth/${name}`);
  }
}
