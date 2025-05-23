import { Routes } from '@angular/router';
import { HomeComponent } from './standalone/pages/home/home.component';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { NotFoundComponent } from './standalone/pages/not-found/not-found.component';
import { ContactUsComponent } from './standalone/pages/contact-us/contact-us.component';
import { VaccinationScheduleComponent } from './standalone/pages/vaccination-schedule/vaccination-schedule.component';
import { roleGuard } from './core/guards/role.guard';
import { PaymentSuccessComponent } from './standalone/components/payment-success/payment-success.component';
import { PaymentCancelComponent } from './standalone/components/payment-cancel/payment-cancel.component';
import { SecondLayoutComponent } from './layout/second-layout/second-layout.component';

export const routes: Routes = [
  {
    path: '',
    component: SecondLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full',
      },
      {
        path: 'home',
        loadComponent: () =>
          import('./standalone/pages/home/home.component').then(
            (c) => c.HomeComponent
          ),
      },
      {
        path: 'contact-us',
        loadComponent: () =>
          import('./standalone/pages/contact-us/contact-us.component').then(
            (c) => c.ContactUsComponent
          ),
      },
      {
        path: 'vaccination-schedule',
        loadComponent: () =>
          import(
            './standalone/pages/vaccination-schedule/vaccination-schedule.component'
          ).then((c) => c.VaccinationScheduleComponent),
      },
    ],
  },

  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.module').then((m) => m.AuthModule),
  },

  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'doctor',
        loadChildren: () =>
          import('./features/doctor/doctor.module').then((m) => m.DoctorModule),
        canActivate: [roleGuard],
        data: { role: 'doctor' },
      },
      {
        path: 'parent',
        loadChildren: () =>
          import('./features/parent/parent.module').then((m) => m.ParentModule),
        canActivate: [roleGuard],
        data: { role: 'parent' },
      },
      {
        path: 'health-ministry',
        loadChildren: () =>
          import(
            './features/health-ministry-admin/health-ministry-admin.module'
          ).then((m) => m.HealthMinistryAdminModule),
        canActivate: [roleGuard],
        data: { role: 'admin' },
      },
      {
        path: 'city-center-admin',
        loadChildren: () =>
          import('./features/city-centre-admin/city-centre-admin.module').then(
            (m) => m.CityCentreAdminModule
          ),
        canActivate: [roleGuard],
        data: { role: 'cityAdmin' },
      },
      {
        path: 'city-admin',
        loadChildren: () =>
          import('./features/city-admin/city-admin.module').then(
            (m) => m.CityAdminModule
          ),
        canActivate: [roleGuard],
        data: { role: 'governorateAdmin' },
      },
      {
        path: 'healthcare-organizer',
        loadChildren: () =>
          import(
            './features/healthcare-organizer/healthcare-organizer.module'
          ).then((m) => m.HeathCareOrganizerModule),
        canActivate: [roleGuard],
        data: { role: 'organizer' },
      },

      // Real-time & Shared Components
      {
        path: 'chat',
        loadComponent: () =>
          import('./standalone/components/real-time/chat/chat.component').then(
            (c) => c.ChatComponent
          ),
        canActivate: [roleGuard],
        data: { roles: ['parent', 'doctor'] }, // أو أي رول مناسب
      },
      {
        path: 'notifications',
        loadComponent: () =>
          import(
            './standalone/components/real-time/notifications/notifications.component'
          ).then((c) => c.NotificationsComponent),
        canActivate: [roleGuard],
        data: { roles: ['parent', 'doctor'] },
      },
      {
        path: 'vaccines',
        loadComponent: () =>
          import('./standalone/components/vaccines/vaccines.component').then(
            (c) => c.VaccinesComponent
          ),
        canActivate: [roleGuard],
        data: { roles: ['governorateAdmin', 'doctor', 'cityAdmin'] },
      },
      {
        path: 'orders',
        loadComponent: () =>
          import('./standalone/components/orders/orders/orders.component').then(
            (c) => c.OrdersComponent
          ),
        canActivate: [roleGuard],
        data: {
          roles: ['governorateAdmin', 'organizer', 'cityAdmin', 'admin'],
        },
      },
      {
        path: 'orders/my-orders',
        loadComponent: () =>
          import(
            './standalone/components/orders/my-orders/my-orders.component'
          ).then((c) => c.MyOrdersComponent),
        canActivate: [roleGuard],
        data: {
          roles: ['governorateAdmin', 'organizer', 'cityAdmin'],
        },
      },

      // Admin Routes
      {
        path: 'admins',
        loadComponent: () =>
          import(
            './standalone/components/administrator/administrators/administrators.component'
          ).then((c) => c.AdministratorsComponent),
        canActivate: [roleGuard],
        data: {
          roles: ['governorateAdmin', 'cityAdmin', 'admin'],
        },
      },
      {
        path: 'admins/add-admin',
        loadComponent: () =>
          import(
            './standalone/components/administrator/add-administrator/add-administrator.component'
          ).then((c) => c.AddAdministratorComponent),
        canActivate: [roleGuard],
        data: {
          roles: ['governorateAdmin', 'cityAdmin', 'admin'],
        },
      },
      {
        path: 'admins/edit-admin/:userId',
        loadComponent: () =>
          import(
            './standalone/components/administrator/edit-administrator/edit-administrator.component'
          ).then((c) => c.EditAdministratorComponent),
        canActivate: [roleGuard],
        data: {
          roles: ['governorateAdmin', 'cityAdmin', 'admin'],
        },
      },
    ],
  },

  { path: 'payment/success', component: PaymentSuccessComponent },
  { path: 'payment/cancel', component: PaymentCancelComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', component: NotFoundComponent },
];
