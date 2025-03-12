import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorCreateScheduleComponent } from './components/doctor-create-schedule/create-schedule.component';
import { DoctorHomePageComponent } from './components/doctor-home-page/doctor-home-page.component';
import { RouterModule, RouterOutlet, Routes } from '@angular/router';
const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('../parent/parent.module').then(m => m.ParentModule)
  },
  { path: 'children', component: DoctorHomePageComponent }

];

@NgModule({
  declarations: [DoctorCreateScheduleComponent, DoctorHomePageComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes), 
  ],
  exports: []
})
export class DoctorModule { }
