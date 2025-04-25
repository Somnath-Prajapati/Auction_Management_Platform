import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-start-page',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './start-page.component.html',
  styleUrls: ['./start-page.component.css']
})
export class StartPageComponent {
  constructor(private router: Router) {}

navigateToGuest() {
  this.router.navigate(['/landing-page']);
}
}
