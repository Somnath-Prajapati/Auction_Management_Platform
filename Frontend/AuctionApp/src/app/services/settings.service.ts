import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  AuctionSettings,
  FinanceSettings,
  DirectSaleSettings,
  StaticPagesSettings,
  FooterLinksSettings
} from '../models/settings';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  private baseUrl = 'https://your-api-url.com/api'; // Replace with your actual backend API base URL

  constructor(private http: HttpClient) {}

  updateAuctionSettings(data: AuctionSettings): Observable<any> {
    return this.http.put(`${this.baseUrl}/AuctionSettings`, data);
  }

  updateFinanceSettings(data: FinanceSettings): Observable<any> {
    return this.http.put(`${this.baseUrl}/FinanceSettings`, data);
  }

  updateDirectSaleSettings(data: DirectSaleSettings): Observable<any> {
    return this.http.put(`${this.baseUrl}/DirectSaleSettings`, data);
  }

  updateStaticPagesSettings(data: StaticPagesSettings): Observable<any> {
    return this.http.put(`${this.baseUrl}/StaticPagesSettings`, data);
  }

  updateFooterLinksSettings(data: FooterLinksSettings): Observable<any> {
    return this.http.put(`${this.baseUrl}/FooterLinksSettings`, data);
  }
}
