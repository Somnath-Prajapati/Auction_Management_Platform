import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-direct-sale',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './direct-sale.component.html',
  styleUrl: './direct-sale.component.css'
})
export class DirectSaleComponent {
  cards = [
    { title: 'Real Estate', icon: 'fas fa-home', description: 'Buy or sell properties quickly and easily.', notification: this.randomBadge() },
    // { title: 'Vehicles', icon: 'fas fa-motorcycle', description: 'Explore listings for cars, bikes, and more.', notification: this.randomBadge() },
    // { title: 'Electronics', icon: 'fas fa-tv', description: 'TVs, phones, gadgets.', notification: this.randomBadge() },
    // { title: 'Fashion', icon: 'fas fa-tshirt', description: 'Clothing & accessories.', notification: this.randomBadge() },
    // { title: 'Jobs', icon: 'fas fa-briefcase', description: 'Find or post jobs.', notification: this.randomBadge() },
    // { title: 'Services', icon: 'fas fa-tools', description: 'Local services.', notification: this.randomBadge() },
    // { title: 'Furniture', icon: 'fas fa-couch', description: 'Home & office furniture.', notification: this.randomBadge() },
  ];

  randomBadge(): number {
    return Math.floor(Math.random() * 100) + 1;
  }
}
