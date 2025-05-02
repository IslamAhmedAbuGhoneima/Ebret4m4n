import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BaseChartDirective } from 'ng2-charts';
import { MinistryAdminDashboardComponent } from './components/ministry-admin-dashboard/ministry-admin-dashboard.component';

const routes: Routes = [
  { path: '', redirectTo: '/health-ministry/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: MinistryAdminDashboardComponent },
  {
    path: 'governorates',
    loadChildren: () =>
      import('./governorates/governorates.module').then(
        (m) => m.GovernoratesModule
      ),
  },
];

@NgModule({
  declarations: [MinistryAdminDashboardComponent],
  imports: [CommonModule, RouterModule.forChild(routes), BaseChartDirective],
})
export class HealthMinistryAdminModule {}
