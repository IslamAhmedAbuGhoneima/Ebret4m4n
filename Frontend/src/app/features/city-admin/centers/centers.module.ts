import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HealthUnitComponent } from './components/health-unit/health-unit.component';
import { CentersHomePageComponent } from './components/centers-home-page/centers-home-page.component';
import { CenterComponent } from './components/center/center.component';

const routes: Routes = [
  { path: '', component: CentersHomePageComponent },
  { path: ':cityName', component: CenterComponent },
  {
    path: ':cityName/:healthCareCenterName/:healthCareCenterId',
    component: HealthUnitComponent,
  },
];

@NgModule({
  declarations: [
    HealthUnitComponent,
    CentersHomePageComponent,
    CenterComponent,
  ],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class CentersModule {}
