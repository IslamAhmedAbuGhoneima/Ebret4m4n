import { Routes } from '@angular/router';
import { HomeComponent } from './standalone/home/home.component';
import { LoginComponent } from './features/auth/Components/login/login.component';
import { RegisterComponent } from './features/auth/Components/register/register.component';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { NotFoundComponent } from './standalone/not-found/not-found.component';

export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      {
        path: 'auth',
        loadChildren: () =>
          import('./features/auth/auth.module').then((m) => m.AuthModule),
      },
      {
        path: 'user',
        loadChildren: () =>
          import('./features/user/user.module').then((m) => m.UserModule),
      },
      {
        path: 'health-ministry',
        loadChildren: () =>
          import(
            './features/admin-of-ministry-of-health/admin-of-ministry-of-health.module'
          ).then((m) => m.AdminOfMinistryOfHealthModule),
      },
      {
        path: 'healthcare',
        loadChildren: () =>
          import('./features/health-care-admin/health-care-admin.module').then(
            (m) => m.HealthCareAdminModule
          ),
      },
    ],
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', component: NotFoundComponent }, //wild card path
];
