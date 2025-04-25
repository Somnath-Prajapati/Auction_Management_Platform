import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Role, Status, User, UserView } from '../../model/user';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})
export class AddUserComponent implements OnInit {

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
    this.router.navigate(['/home/users']);
    console.log('Add new user modal opened');
  }

  // Method to edit a user
  editUser(user: UserView): void {

    // For now, we'll just log to console
    console.log('Editing user:', user);
    alert(`Would edit user with UID: ${user.email}`);
  }

  deleteUser(userId: string): void {
    const confirmDelete = confirm('Are you sure you want to delete this user?');
    
    if (confirmDelete) {
      console.log('Deleting user with ID:', userId);
      alert(`Would delete user with UID: ${userId}`);
      
    }
  }

}
