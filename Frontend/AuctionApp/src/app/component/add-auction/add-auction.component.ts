import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';


@Component({
  selector: 'app-add-auction',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-auction.component.html',
  styleUrls: ['./add-auction.component.css']
})
export class AddAuctionComponent {
  auctionForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.auctionForm = this.fb.group({
      auctionNumber: ['', Validators.required],
      title: ['', Validators.required],
      type: ['', Validators.required],
      startDateTime: ['', Validators.required],
      endDateTime: ['', Validators.required],
      statusId: ['', Validators.required],
      incrementalTime: ['', Validators.required],
      categoryId: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.auctionForm.valid) {
      const formValue = this.auctionForm.value;

      const payload = {
        auctionNumber: formValue.auctionNumber,
        title: formValue.title,
        type: formValue.type,
        startDateTime: new Date(formValue.startDateTime).toISOString(),
        endDateTime: new Date(formValue.endDateTime).toISOString(),
        statusId: +formValue.statusId,
        incrementalTime: +formValue.incrementalTime,
        categoryId: +formValue.categoryId
      };

      this.http.post('https://localhost:50264/api/Auction', payload)
        .subscribe({
          next: res => {
            alert('Auction created successfully');
            this.auctionForm.reset();
          },
          error: err => {
            console.error(err);
            alert('Something went wrong');
          }
        });
    }
  }
}
