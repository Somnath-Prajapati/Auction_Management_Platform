import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestServices } from '../../Services/RequestServices';
import { RequestDetail } from '../../Models/RequestDetail';

@Component({
  selector: 'app-request-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './request-detail.component.html',
  styleUrls: ['./request-detail.component.css'],
})
export class RequestDetailComponent implements OnInit {
  requestId!: number;
  requestData: RequestDetail = {} as RequestDetail;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private requestService: RequestServices
  ) {}

  ngOnInit(): void {
    this.requestId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.requestId === 0) {
      this.requestData = {} as RequestDetail;
    } else {
      // Fetch the request details if it's not a new request
      this.requestService.getRequestById(this.requestId).subscribe({
        next: (data) => (this.requestData = data),
        error: (err) => console.error('Error loading request:', err),
      });
    }
  }

  updateRequest(): void {
    if (this.requestId === 0) {
      // Create new request
      this.requestService.createRequest(this.requestData).subscribe({
        next: () => {
          alert('New Request Created!');
          this.router.navigate(['/requests']);
        },
        error: (err) => console.error('Error creating request:', err),
      });
    } else {
      // Update existing request
      this.requestService.updateRequest(this.requestId, this.requestData).subscribe({
        next: () => {
          alert('Request updated successfully!');
          this.router.navigate(['/requests']);
        },
        error: (err) => console.error('Error updating request:', err),
      });
    }
  }
  
}