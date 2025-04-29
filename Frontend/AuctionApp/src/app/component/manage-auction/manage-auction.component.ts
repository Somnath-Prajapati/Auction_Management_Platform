import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Auction, AuctionService } from '../../services/auction.service';
import { RouterModule } from '@angular/router';

declare const bootstrap: any;

@Component({
  selector: 'app-manage-auction',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './manage-auction.component.html',
  styleUrls: ['./manage-auction.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ManageAuctionComponent implements OnInit {
  auctions: Auction[] = [];
  selectedAuction: Auction | null = null;

  constructor(private auctionService: AuctionService) {}

  ngOnInit(): void {
    this.fetchAuctions();
  }

  fetchAuctions() {
    this.auctionService.getAllAuctions().subscribe({
      next: data => this.auctions = data,
      error: err => console.error('Error fetching auctions', err)
    });
  }

  openDeleteModal(auction: Auction) {
    this.selectedAuction = auction;
    const modalEl = document.getElementById('deleteAuctionModal');
    if (modalEl) {
      const modal = new bootstrap.Modal(modalEl);
      modal.show();
    }
  }

  deleteAuctionConfirmed() {
    if (!this.selectedAuction) return;
    this.auctionService.deleteAuction(this.selectedAuction.auctionId).subscribe({
      next: () => {
        this.auctions = this.auctions.filter(a => a.auctionId !== this.selectedAuction?.auctionId);
        const modalInstance = bootstrap.Modal.getInstance(document.getElementById('deleteAuctionModal')!);
        modalInstance?.hide();
        this.selectedAuction = null;
      },
      error: err => {
        console.error('Error deleting auction', err);
        alert('Failed to delete auction.');
      }
    });
  }
}
