
  import { Component, OnInit,ViewChild } from '@angular/core';
  import { UserService } from '../../services/user.service';
  import { Role, Status, User, UserView } from '../../model/user';
  import { CommonModule } from '@angular/common';
  import { Router } from '@angular/router';

  @Component({
    selector: 'app-manage-user',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './manage-user.component.html',
    styleUrls: ['./manage-user.component.css'] 
  })
  export class ManageUserComponent  implements OnInit {

    users: UserView[] = [];
    roles: Role[] = [];
    statuses: Status[] = [];

    constructor(private userService: UserService,private router:Router) { }

    ngOnInit(): void {
      this.loadUsers();
      this.loadRoles();
      this.loadStatuses();
    }

    loadUsers(): void {
      this.userService.getAllUser().subscribe(data => this.users = data);
    }

    loadRoles(): void {
      this.userService.getRoles().subscribe(data => this.roles = data);
    }

    loadStatuses(): void {
      this.userService.getStatuses().subscribe(data => this.statuses = data);
    }

    getRoleName(roleId: number): string {
      return this.roles.find(r => r.roleId === roleId)?.roleName || 'Unknown';
    }

    getStatusName(statusId: number): string {
      return this.statuses.find(s => s.statusId === statusId)?.statusName || 'Unknown';
    }
    openAddUserModal(): void {
      this.router.navigate(['/home/newUser']);
    }

    editUser(user: UserView): void {
      if (user && user.userId) {
        const encodedUserId = btoa(user.userId.toString()); // base64 encode
        this.router.navigate(['/home/updateUser'], { queryParams: { id: encodedUserId } });
      } else {
        console.error('Invalid userId');
      }
    }  
    viewUser(user: UserView): void {
      if (user && user.userId) {
        const encodedUserId = btoa(user.userId.toString()); // base64 encode
        this.router.navigate(['/home/detailsUser'], { queryParams: { id: encodedUserId } });
      } else {
        console.error('Invalid userId');
      }
    }    

    deleteUser(userId:number):void {
      this.userService.deleteUser(userId).subscribe({
        next: (response) =>{
        console.log('User added successfully!', response);
        this.loadUsers();
        },
          
        error: (error) => console.error('Error adding user:', error)
      });
    }
    

  }

