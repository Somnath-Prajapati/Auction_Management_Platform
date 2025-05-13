import { Routes } from '@angular/router';
import { RequestComponent } from './components/request/request.component'; // Correct path
import { RequestDetailComponent } from './components/request-detail/request-detail.component';
import { NewRequestComponent } from './request-components/new-request/new-request.component';

export const routes: Routes = [
  { path: 'request', component: RequestComponent }, // Ensure path matches
  { path: 'request-detail/:id', component: RequestDetailComponent },
  { path: 'requests/new', component: NewRequestComponent  },
  { path: '', redirectTo: '/request', pathMatch: 'full' } // Redirect to 'request' on root
];
