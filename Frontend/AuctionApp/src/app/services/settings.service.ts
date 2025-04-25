import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin } from 'rxjs';
import {
  AuctionSettings,
  FinanceSettings,
  DirectSaleSettings,
  StaticPagesSettings,
  FooterLinksSettings,
  AllSettings
} from '../modals/settings';

@Injectable({
  providedIn: 'root',
})
export class SettingsService {
  private baseUrl = 'https://10.0.102.118:44384/api';

  constructor(private http: HttpClient) {}

  // Individual GET methods
  getAuctionSettings(): Observable<AuctionSettings> {
    return this.http.get<AuctionSettings>(`${this.baseUrl}/AuctionSettings`);
  }

  getFinanceSettings(): Observable<FinanceSettings> {
    return this.http.get<FinanceSettings>(`${this.baseUrl}/FinanceSettings/GetAll`);
  }

  getDirectSaleSettings(): Observable<DirectSaleSettings> {
    return this.http.get<DirectSaleSettings>(`${this.baseUrl}/DirectSaleSettings`);
  }

  getStaticPagesSettings(): Observable<StaticPagesSettings> {
    return this.http.get<StaticPagesSettings>(`${this.baseUrl}/StaticPagesSettings`);
  }

  getFooterLinksSettings(): Observable<FooterLinksSettings> {
    return this.http.get<FooterLinksSettings>(`${this.baseUrl}/FooterLinksSettings/Get`);
  }

  // Combined GET method
  getAllSettings(): Observable<AllSettings> {
    return forkJoin({
      auctionSettings: this.getAuctionSettings(),
      financeSettings: this.getFinanceSettings(),
      directSaleSettings: this.getDirectSaleSettings(),
      staticPages: this.getStaticPagesSettings(),
      footerLinks: this.getFooterLinksSettings()
    });
  }

  // Individual UPDATE methods
  updateAuctionSettings(settings: AuctionSettings): Observable<any> {
    return this.http.put(`${this.baseUrl}/AuctionSettings`, settings);
  }

  updateFinanceSettings(settings: FinanceSettings): Observable<any> {
    return this.http.put(`${this.baseUrl}/FinanceSettings`, settings);
  }

  updateDirectSaleSettings(settings: DirectSaleSettings): Observable<any> {
    return this.http.put(`${this.baseUrl}/DirectSaleSettings`, settings);
  }

  updateStaticPagesSettings(settings: StaticPagesSettings): Observable<any> {
    return this.http.put(`${this.baseUrl}/StaticPagesSettings`, settings);
  }

  updateFooterLinksSettings(settings: FooterLinksSettings): Observable<any> {
    return this.http.put(`${this.baseUrl}/FooterLinksSettings`, settings);
  }
}
