import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AddUnitComponent } from './components/add-unit/add-unit.component';
import { HealthUnitComponent } from './components/health-unit/health-unit.component';
import { UnitsHomePageComponent } from './components/units-home-page/units-home-page.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  { path: '', component: UnitsHomePageComponent },

  { path: 'add-unit', component: AddUnitComponent },
  {
    path: ':healthCareCenterName/:healthCareCenterId',
    component: HealthUnitComponent,
  },
];

@NgModule({
  declarations: [AddUnitComponent, HealthUnitComponent, UnitsHomePageComponent],
  imports: [CommonModule, RouterModule.forChild(routes), MatDialogModule,ReactiveFormsModule],
})
export class UnitsModule {}
