import { Routes } from '@angular/router';
import { HomeComponent } from './standalone/pages/home/home.component';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { NotFoundComponent } from './standalone/components/not-found/not-found.component';

export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      {
        path: 'doctor',
        loadChildren: () =>
          import('./features/doctor/doctor.module').then((m) => m.DoctorModule),
      },
      {
        path: 'parent',
        loadChildren: () =>
          import('./features/parent/parent.module').then((m) => m.ParentModule),
      },
      {
        path: 'health-ministry',
        loadChildren: () =>
          import(
            './features/health-ministry-admin/health-ministry-admin.module'
          ).then((m) => m.HealthMinistryAdminModule),
      },
      {
        path: 'city-centre-admin',
        loadChildren: () =>
          import('./features/city-centre-admin/city-centre-admin.module').then(
            (m) => m.CityCentreAdminModule
          ),
      },
      {
        path: 'healthcare-organizer',
        loadChildren: () =>
          import('./features/healthcare-organizer/healthcare-organizer.module').then(
            (m) => m.HeathCareOrganizerModule
          ),
      },
    ],

  }, {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.module').then((m) => m.AuthModule),
  },

  { path: '**', component: NotFoundComponent }, //wild card path
];
