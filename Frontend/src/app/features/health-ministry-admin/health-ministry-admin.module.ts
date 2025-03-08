import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HealthMinistryAdminHomePageComponent } from './components/health-ministry-admin-home-page/health-ministry-admin-home-page.component';


 const routes: Routes = [
  { path: '', redirectTo: '/health-ministry/home', pathMatch: 'full' },
  { path: 'home', component: HealthMinistryAdminHomePageComponent },
];

@NgModule({
  declarations: [HealthMinistryAdminHomePageComponent],
  imports: [
    CommonModule, RouterModule.forChild(routes)
  ]
})
export class HealthMinistryAdminModule { }
