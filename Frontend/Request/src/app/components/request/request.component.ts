import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { RequestServices } from '../../Services/RequestServices';
import { Request } from '../../Models/Request';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-requestcomponents',
  standalone: true,
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css'],
  imports: [CommonModule, RouterModule, FormsModule]
})
export class RequestComponent implements OnInit {
  requestList: Request[] = [];
  searchTerm = '';
  filterType = 0;
  filterStatus = 0;

  constructor(
    private requestService: RequestServices,
    private router: Router,
    private cdr: ChangeDetectorRef // Inject ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll() {
    this.requestService.getAllRequests().subscribe({
      next: data => this.requestList = data,
      error: err => console.error('Error fetching requests', err)
    });
  }

  get filteredRequests(): Request[] {
    return this.requestList
      .filter(r => {
        const term = this.searchTerm.toLowerCase();
        return !this.searchTerm
          || r.requestNumber.toLowerCase().includes(term)
          || r.username.toLowerCase().includes(term);
      })
      .filter(r => !this.filterType || r.requestTypeId === this.filterType)
      .filter(r => !this.filterStatus || r.requestStatusId === this.filterStatus);
  }

  getTypeName(typeId: number): string {
    switch (typeId) {
      case 1: return 'Transfer of Ownership';
      case 2: return 'Inquiry';
      case 3: return 'Request for Viewing';
      case 4: return 'Offer';
      default: return 'Unknown';
    }
  }

  getStatusName(statusId: number): string {
    switch (statusId) {
      case 1: return 'Pending';
      case 2: return 'Done';
      case 3: return 'Approved';
      case 4: return 'Rejected';
      default: return 'Unknown';
    }
  }

  deleteRequest(id: number) {
    if (!confirm('Are you sure you want to delete this request?')) return;
  
    this.requestService.deleteRequest(id).subscribe({
      next: () => {
        alert('Deleted successfully');
        this.loadAll();   // 🔥 Reload the table data after delete
        this.cdr.detectChanges(); // Manually trigger change detection
      },
      error: err => console.error('Delete failed', err)
    });
  }
  
}
