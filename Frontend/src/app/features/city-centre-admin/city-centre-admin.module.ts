import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CityAdminDashboardComponent } from '../city-admin/components/city-admin-dashboard/city-admin-dashboard.component';
import { CityCenterAdminDashboardComponent } from './components/city-center-admin-dashboard/city-center-admin-dashboard.component';
import { BaseChartDirective } from 'ng2-charts';

const routes: Routes = [
  { path: '', redirectTo: '/city-centre-admin/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: CityCenterAdminDashboardComponent },
  {
    path: 'units',
    loadChildren: () =>
      import('./units/units.module').then((m) => m.UnitsModule),
  },
  {
    path: 'doctors',
    loadChildren: () =>
      import('./doctors/doctors.module').then((m) => m.DoctorsModule),
  },
  {
    path: 'complaints',
    loadChildren: () =>
      import('./complaints/complaints.module').then((m) => m.ComplaintsModule),
  },
];
@NgModule({
  declarations: [CityCenterAdminDashboardComponent],
  imports: [CommonModule, RouterModule.forChild(routes), BaseChartDirective],
  exports: [],
})
export class CityCentreAdminModule {}
