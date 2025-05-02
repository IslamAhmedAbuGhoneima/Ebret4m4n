import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { CityAdminDashboardComponent } from './components/city-admin-dashboard/city-admin-dashboard.component';
import { BaseChartDirective } from 'ng2-charts';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: CityAdminDashboardComponent },

  {
    path: 'centers',
    loadChildren: () =>
      import('./centers/centers.module').then((m) => m.CentersModule),
  },
];

@NgModule({
  declarations: [CityAdminDashboardComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    MatDialogModule,
    FormsModule,
    BaseChartDirective,
  ],
  exports: [],
})
export class CityAdminModule {}
