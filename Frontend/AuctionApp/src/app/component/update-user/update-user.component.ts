import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Country, Role, Status } from '../../model/user';
import { UserService } from '../../services/user.service';


@Component({
  selector: 'app-update-user',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {
  userForm!: FormGroup;
  userId!: number;
  roles: Role[] = [];
  statuses: Status[] = [];
  countries: Country[] = [];

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('id'));
    this.initForm();
    this.loadDropdownData();
    this.loadUserData();
  }

  initForm() {
    this.userForm = this.fb.group({
      name: [''],
      email: [''],
      mobileNumber: [''],
      companyName: [''],
      roleId: [''],
      statusId: [''],
      gender: [''],
      countryId: ['']
    });
  }

  loadDropdownData() {
    this.userService.getRoles().subscribe(data => this.roles = data);
    this.userService.getStatuses().subscribe(data => this.statuses = data);
    this.userService.getCountry().subscribe(data => this.countries = data);
  }

  loadUserData() {
    this.userService.getAllUser().subscribe(users => {
      const user = users.find(u => u.userId === this.userId);
      if (user) {
        this.userForm.patchValue(user);
      }
    });
  }

  onSubmit() {
    const formData = new FormData();
    Object.entries(this.userForm.value).forEach(([key, value]) => {
      if (value !== null && value !== undefined) {
        formData.append(key, value.toString());
    }
    });

    this.userService.updateUser(this.userId, formData).subscribe(() => {
      alert('User updated successfully');
      this.router.navigate(['/users']);
    });
  }
}
