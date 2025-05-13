// import { Component } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { RouterModule, Router } from '@angular/router';

// import { RequestServices } from '../../Services/RequestServices';
// import { RequestDetail } from '../../Models/RequestDetail';

// @Component({
//   selector: 'app-new-request',
//   standalone: true,
//   imports: [CommonModule, FormsModule, RouterModule],
//   templateUrl: './new-request.component.html',
//   styleUrls: ['./new-request.component.css']
// })
// export class NewRequestComponent {
//   requestData: RequestDetail = {
//     requestNumber: '',
//     userId: 0,
//     username: '',
//     mobileNumber: '',
//     email: '',
//     requestTypeId: 1,
//     assetId: 0,
//     transactionId: undefined,
//     requestDateTime: new Date().toISOString().substring(0,16),
//     requestStatusId: 1,
//     customerNote: '',
//     adminNote: '',
//     createdByAdmin: false
//   };

//   constructor(
//     private requestService: RequestServices,
//     private router: Router
//   ) {}

//   createNew(): void {
//     this.requestService.createRequest(this.requestData).subscribe({
//       next: () => {
//         alert('Request created successfully!');
//         this.router.navigate(['/requests']);
//       },
//       error: err => {
//         console.error('Error creating request', err);
//         alert('Failed to create request.');
//       }
//     });
//   }

//   cancel(): void {
//     this.router.navigate(['/requests']);
//   }
// }



import { Component, OnInit } from '@angular/core';
import { RequestServices } from '../../Services/RequestServices';
import { CreateRequest } from '../../Models/CreateRequest'; // model for creating request
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-new-request',
  standalone: true,
  templateUrl: './new-request.component.html',
  styleUrls: ['./new-request.component.css'],
  imports: [CommonModule, RouterModule, FormsModule]

})
export class NewRequestComponent implements OnInit {
  newRequest: CreateRequest = {} as CreateRequest;

  constructor(
    private requestService: RequestServices,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.generateRequestNumber(); // 🧠 Generate request number on load
  }

  generateRequestNumber() {
    const today = new Date();
    const yyyyMMdd = today.getFullYear().toString() +
      (today.getMonth() + 1).toString().padStart(2, '0') +
      today.getDate().toString().padStart(2, '0');

    const randomThreeDigit = Math.floor(1 + Math.random() * 999).toString().padStart(3, '0');

    this.newRequest.requestNumber = `REQ-${yyyyMMdd}-${randomThreeDigit}`;
  }

  createNew(): void {
    this.requestService.createRequest(this.newRequest).subscribe({
      next: () => {
        alert('Request created successfully!');
        this.router.navigate(['/requests']);
      },
      error: (err) => {
        console.error('Error creating request', err);
        alert('Failed to create request.');
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/requests']);
  }
}

 
