import { Routes } from '@angular/router';
import { authGuard, adminGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { TicketListComponent } from './components/ticket-list/ticket-list.component';
import { TicketFormComponent } from './components/ticket-form/ticket-form.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { 
    path: 'tickets', 
    component: TicketListComponent,
    canActivate: [authGuard]
  },
  { 
    path: 'tickets/create', 
    component: TicketFormComponent,
    canActivate: [authGuard]
  },
  { 
    path: 'tickets/edit/:id', 
    component: TicketFormComponent,
    canActivate: [authGuard]
  },
  { 
    path: 'admin/tickets', 
    component: TicketListComponent,
    canActivate: [adminGuard]
  },
  { path: '**', redirectTo: '/login' }
];
