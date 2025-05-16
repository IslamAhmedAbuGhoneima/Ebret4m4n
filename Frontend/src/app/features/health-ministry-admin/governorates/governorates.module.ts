import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { GovernoratesListComponent } from './components/governorates-list/governorates-list.component';
import { GovernorateComponent } from './components/governorate/governorate.component';
import { CenterComponent } from './components/center/center.component';
import { HealthUnitComponent } from './components/health-unit/health-unit.component';

const routes: Routes = [
  { path: '', component: GovernoratesListComponent },
  {
    path: ':governorateName',
    component: GovernorateComponent,
  },
  {
    path: ':governorateName/:centerName/:centerId',
    component: CenterComponent,
  },
  {
    path: ':governorateName/:centerName/:centerId/:healthCareCenterName/:healthCareCenterId',
    component: HealthUnitComponent,
  },
];

@NgModule({
  declarations: [
    GovernoratesListComponent,
    GovernorateComponent,
    CenterComponent,
    HealthUnitComponent,
  ],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class GovernoratesModule {}
