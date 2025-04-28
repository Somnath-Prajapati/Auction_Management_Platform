import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
<<<<<<< HEAD:Frontend/Request/src/app/app.config.ts
=======
import { provideHttpClient } from '@angular/common/http';
>>>>>>> a1847a9552287d62e92e6ab23bb2171e4632a0fd:Frontend/AuctionApp/src/app/app.config.ts
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
<<<<<<< HEAD:Frontend/Request/src/app/app.config.ts
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes), provideClientHydration(), provideHttpClient()]
=======
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes)
    ,provideHttpClient()
  ]
>>>>>>> a1847a9552287d62e92e6ab23bb2171e4632a0fd:Frontend/AuctionApp/src/app/app.config.ts
};
