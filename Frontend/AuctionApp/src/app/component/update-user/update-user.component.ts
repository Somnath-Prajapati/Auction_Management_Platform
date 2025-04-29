import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserView } from '../../model/user';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm, NgModel } from '@angular/forms';
import { FutureDateValidatorDirective } from '../manage-user/future-date-validator.directive';

@Component({
  selector: 'app-update-user',
  standalone: true,
  imports: [CommonModule, FutureDateValidatorDirective, FormsModule],
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {
  user: UserView = {} as UserView;
  countries: any[] = [];
  statuses: any[] = [];
  roles: any[] = [];
  selectedPhoneCode: string = '';
  profileImageFile: File | null = null;
  personalIdImageFile: File | null = null;
  userId!: number;
  @ViewChild('userForm') userForm!: NgForm;

  submitted = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
  ) { }

  ngOnInit(): void {
    const userId = this.route.snapshot.queryParams['id'] ? +atob(this.route.snapshot.queryParams['id']) : null;
    if (userId !== null) {
      this.userId = userId;
    } else {
      console.error('User ID not found in URL');
    }

    this.loadUser();
    this.loadCountries();
    this.loadDropdownData();
  }

  loadUser() {
    this.userService.getUserById(this.userId).subscribe((data: UserView) => {
      this.user = data;
      const selectedCountry = this.countries.find(country => country.countryId === this.user.countryId);
      if (selectedCountry) {
        this.selectedPhoneCode = selectedCountry.phoneCode;
      }

    });
  }


  loadCountries() {
    this.userService.getCountry().subscribe((data: any[]) => {
      this.countries = data;
      if (this.user.countryId) {
        const selectedCountry = this.countries.find(country => country.countryId === this.user.countryId);
        if (selectedCountry) {
          this.selectedPhoneCode = selectedCountry.phoneCode;
        }
      }
    });
  }
  loadDropdownData() {
    this.userService.getStatuses().subscribe(data => {
      this.statuses = data;
    });
    this.userService.getRoles().subscribe(data => {
      this.roles = data;
    });
  }

  onProfileImageSelected(event: any) {
    this.profileImageFile = event.target.files[0];
  }

  onPersonalIdImageSelected(event: any) {
    this.personalIdImageFile = event.target.files[0];
  }
  getCountryName(countryId: number): string {
    const country = this.countries.find(c => c.countryId === countryId);
    return country ? country.countryName : '';
  }

  onUpdateUser(form: any) {
    this.submitted = true;
    if (this.userForm.invalid) {
      Object.values(this.userForm.controls).forEach(control => {
        control.markAsTouched();
      });
      return; // Stop the form submission if there are invalid fields
    }

    // Ensure required images are selected before proceeding
    if (!this.profileImageFile && !this.user.profileImageUrl) {
      alert('Please upload a profile image.');
      return;
    }
    if (!this.personalIdImageFile && !this.user.personalIdImageUrl) {
      alert('Please upload a personal ID image.');
      return;
    }

    const formData = new FormData();

    // Append user form fields to FormData
    for (let key in this.user) {
      const value = (this.user as any)[key];
      if (value !== null && value !== undefined) {
        formData.append(key, value);
      }
    }

    // Append profile image if selected
    if (this.profileImageFile) {
      formData.append('profileImage', this.profileImageFile);
    }

    // Append personal ID image if selected
    if (this.personalIdImageFile) {
      formData.append('personalIdImage', this.personalIdImageFile);
    }

    // Call service to update the user data
    this.userService.updateUser(this.userId, formData).subscribe({
      next: (res) => {
        console.log('User updated successfully', res);
        this.router.navigate(['/home/users']);
      },
      error: (err) => {
        console.error('Update failed', err);
        alert('There was an error updating the user. Please try again.');
      }
    });
  }
}