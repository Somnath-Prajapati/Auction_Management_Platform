import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  // Stat card data (can be made dynamic later)
  totalRevenue = '250,000 BHD';
  totalAuctions = 12540;
  thisMonthRevenue = '25,000 BHD';

  // Recent activities
  recentActivities = [
    { message: 'Auction "Rolex Watch" created.', type: 'purple' },
    { message: 'Payment of 3,000 BHD received.', type: 'green' },
    { message: 'Auction "Land in Manama" completed.', type: 'blue' },
  ];

  // Top categories (static list for now)
  topCategories = ['Electronics', 'Cars', 'Watches', 'Real Estate'];

  // Auction highlights (can be fetched from an API later)
  auctionHighlights = [
    { name: 'Luxury Villa', status: 'Active', bids: 54, revenue: '85,000 BHD' },
    { name: 'Vintage Car', status: 'Ended', bids: 39, revenue: '40,000 BHD' },
    { name: 'Smartphone Bundle', status: 'Upcoming', bids: '-', revenue: '-' },
  ];
}
