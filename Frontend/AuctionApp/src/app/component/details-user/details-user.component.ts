import { Component } from '@angular/core';
import { Country, Role, Status, UserView } from '../../model/user';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-details-user',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './details-user.component.html',
  styleUrl: './details-user.component.css'
})
export class DetailsUserComponent {
  user: UserView = {} as UserView;
  countries: Country[] =[];
  roles: Role[] = [];
  statuses: Status[] = [];
  userId!: number;

  constructor(private userService: UserService,private route: ActivatedRoute,private router: Router, ) {}
  
  ngAfterViewInit(): void {
    // this.userId = +this.route.snapshot.paramMap.get('userId')!;
    // Retrieve the userId from the state passed through the router
    this.route.queryParams.subscribe(params => {
      const encryptedId = params['id'];
      if (encryptedId) {
        this.userId = +atob(encryptedId); // base64 decode
        this.getUserById(this.userId);
      } else {
        console.error('User ID not found in URL');
      }
    });

    // this.getUserById(this.userId);
    this.loadDropdowns();
    this.getCountryName(this.user.countryId);
  }
  getUserById(id: number): void {
    this.userService.getUserById(id).subscribe({
      next: (data) => {
        this.user = data;
        if (this.user && this.user.countryId) {
          this.getCountryName(this.user.countryId);
        }
      },
      error: (error) => {
        console.error('Error fetching user data:', error);
      }
    });
  }
  loadDropdowns(): void {
    this.userService.getRoles().subscribe(data => {
      console.log('Roles:', data);
      this.roles = data;
    });
    this.userService.getStatuses().subscribe(data => this.statuses = data);
    this.userService.getCountry().subscribe(data => this.countries = data);
  }
  
  getCountryName(countryId: number): string {
    const UserCountry = this.countries.find(c => c.countryId === this.user.countryId);
    console.log(UserCountry)
    return UserCountry ? UserCountry.countryName : '';
  }
  getRoleName(RoleId:number):string{
    const UserRole = this.roles.find(r => r.roleId == this.user.roleId);
    return UserRole ? UserRole.roleName : '';
  }
  getStatusName(StatusId:number):string{
    const status = this.statuses.find(c => c.statusId === this.user.statusId);
    return status ? status.statusName : '';
  }
  
}
