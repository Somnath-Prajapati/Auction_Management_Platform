import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Auction {
  auctionId: number;
  auctionNumber: string;
  title: string;
  type: string;
  startDateTime: string;
  endDateTime: string;
  statusId: number;
  incrementalTime: number;
  categoryId: number;
}

@Injectable({
  providedIn: 'root'
})
export class AuctionService {
  private apiUrl = 'https://localhost:50264/api/Auction'; // Replace with your backend URL if different

  constructor(private http: HttpClient) {}

  getAllAuctions(): Observable<Auction[]> {
    return this.http.get<Auction[]>(this.apiUrl);
  }
  deleteAuction(id: number) {
    return this.http.delete(`this.apiUrl/${id}`);
  }
  
}
