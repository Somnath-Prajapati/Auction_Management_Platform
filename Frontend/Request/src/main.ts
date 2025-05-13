import { bootstrapApplication } from '@angular/platform-browser';
<<<<<<< HEAD:Frontend/AuctionApp/src/main.ts
=======
import { appConfig } from './app/app.config'; // appConfig file for the routing setup
>>>>>>> e71cff9a9339ae9e3bf3ecf10da7fe1fe248b49a:Frontend/Request/src/main.ts
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(AppComponent, {
  ...appConfig,
  providers: [
    ...(appConfig.providers || []),
    provideHttpClient()
  ]
}).catch(err => console.error(err));
