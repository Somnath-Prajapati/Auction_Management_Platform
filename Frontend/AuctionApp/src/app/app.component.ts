import { Component } from '@angular/core';
import { Router, NavigationEnd, RouterOutlet, RouterModule } from '@angular/router';
import { filter } from 'rxjs/operators';
import { NgIf } from '@angular/common';
import { HeaderComponent } from "./component/header/header.component";
import { FooterComponent } from "./component/footer/footer.component";
import { SidebarComponent } from "./component/sidebar/sidebar.component";
import { HomeComponent } from "./component/home/home.component";
import { routes } from './app.routes';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  currentRoute: string = '';

  constructor(private router: Router) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.currentRoute = event.urlAfterRedirects;
    });
  }

  get isStartPage(): boolean {
    return this.currentRoute === '/';
  }

  get isLandingPage(): boolean {
    return this.currentRoute === '/landing-page';
  }

  get showSidebar(): boolean {
    return !this.isStartPage && !this.isLandingPage;
  }

  get showHeaderAndFooter(): boolean {
    return !this.isStartPage;
  }
}
