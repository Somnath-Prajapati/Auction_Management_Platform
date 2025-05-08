import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app.routes';
<<<<<<< HEAD:Frontend/AuctionApp/src/app/app.config.ts
import { HttpClient } from '@angular/common/http';
=======
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
>>>>>>> e71cff9a9339ae9e3bf3ecf10da7fe1fe248b49a:Frontend/Request/src/app/app.config.ts

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes), provideClientHydration(), provideHttpClient()]
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes)
    ,provideHttpClient()
  ]
};
