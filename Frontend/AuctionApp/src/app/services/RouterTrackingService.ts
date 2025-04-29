import { Router, NavigationStart, NavigationEnd } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()
export class RouterTrackingService {
  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        console.log('Navigation started', event);
      }

      if (event instanceof NavigationEnd) {
        console.log('Navigation ended', event);
      }
    });
  }
}
