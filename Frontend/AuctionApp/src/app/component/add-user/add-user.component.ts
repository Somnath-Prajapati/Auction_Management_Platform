import { Component, OnInit } from '@angular/core';
import { Country, Role, Status, User } from '../../modals/add-user';
import { UserService } from '../../services/add-user';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})
export class AddUserComponent implements OnInit {
  user: User = {
    name: '',
    mobileNumber: '',
    email: '',
    companyName: '',
    companyNumber: '',
    statusId: 0,
    chatEnabled: true,
    roleId: 0,
    personalIdNumber: '',
    gender: 'male',
    personalIdExpiryDate: '',
    countryId: 0,
    profileImage: null,
    personalIdImage: null
  };
  roles: Role[] = [];
  statuses: Status[] = [];
  countries:Country[] =[];
  selectedPhoneCode: string = '';
 
    constructor(private userService: UserService) {}
    ngOnInit(): void {
    this.loadDropdowns();
    }
    onFileChange(event: Event, field: 'profileImage' | 'personalIdImage'): void {
      const input = event.target as HTMLInputElement;
      if (input.files && input.files.length > 0) {
        this.user[field] = input.files[0];
      }
    }
    loadDropdowns(): void {
      this.userService.getRoles().subscribe(data =>{console.log('Roles:', data);this.roles = data} );
      this.userService.getStatuses().subscribe(data => this.statuses = data);
      this.userService.getCountry().subscribe(data => this.countries = data);
    }
 
    onSubmit(): void {
      const formData = new FormData();
      for (const key in this.user) {
        const value = (this.user as any)[key];
        if (value instanceof File) {
          formData.append(key, value);
        } else {
          formData.append(key, value.toString());
        }
      }
 
      this.userService.addUser(formData).subscribe({
        next: (response) => console.log('User added successfully!', response),
        error: (error) => console.error('Error adding user:', error)
      });
}
}
