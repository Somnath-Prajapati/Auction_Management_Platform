import { Component } from '@angular/core';

// Import child components
import { HeaderComponent } from '../header/header.component';
import { FooterComponent } from '../footer/footer.component';
import { PromotionsComponent } from './promotions/promotions.component';
import { DirectSaleComponent } from './direct-sale/direct-sale.component';
import { CategoryCardComponent } from './category-card/category-card.component';

@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [
    PromotionsComponent,
    DirectSaleComponent,
    CategoryCardComponent
  ],
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent {}
