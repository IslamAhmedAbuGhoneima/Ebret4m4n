import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorCreateScheduleComponent } from './components/doctor-create-schedule/create-schedule.component';
import { DoctorHomePageComponent } from './components/doctor-home-page/doctor-home-page.component';
import { RouterModule, RouterOutlet, Routes } from '@angular/router';
import { ParentModule } from '../parent/parent.module';
export const routes: Routes = [
  { path: '', redirectTo: '/doctor/home', pathMatch: 'full' },
  { path: 'home', component: DoctorHomePageComponent },
];


@NgModule({
  declarations: [DoctorCreateScheduleComponent, DoctorHomePageComponent],
  imports: [RouterModule.forChild(routes),
    CommonModule, ParentModule
  ]
})
export class DoctorModule { }
