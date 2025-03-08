import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { AddChildPageComponent } from './components/add-child-page/add-child-page.component';
import { ChildHealthRecordComponent } from './components/child-health-record/child-health-record.component';
import { ChildScheduleComponent } from './components/child-schedule/child-schedule.component';
import { ComplaintPageComponent } from './components/complaint-page/complaint-page.component';
import { ParentChildrenPageComponent } from './components/parent-children-page/parent-children-page.component';
import { ParentHomePageComponent } from './components/parent-home-page/parent-home-page.component';
import { ParentNotificationComponent } from './components/parent-notification/parent-notification.component';
import { VaccineSideEffectComponent } from './components/vaccine-side-effect/vaccine-side-effect.component';
import { BookAppointmentComponent } from './components/book-appointment/book-appointment.component';
import { UpdateAppointmentComponent } from './components/update-appointment/update-appointment.component';
import { RouterModule, Routes } from '@angular/router';
export const routes: Routes = [
  { path: '', redirectTo: '/parent/home', pathMatch: 'full' },
  { path: 'home', component: ParentHomePageComponent },
];

@NgModule({
  declarations: [
    AddChildPageComponent,
    ChildHealthRecordComponent,
    ChildScheduleComponent,
    ComplaintPageComponent,
    ParentChildrenPageComponent,
    ParentHomePageComponent,
    ParentNotificationComponent,
    VaccineSideEffectComponent,
    BookAppointmentComponent,
    UpdateAppointmentComponent,
  ],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [
    AddChildPageComponent,
    ChildHealthRecordComponent,
    ChildScheduleComponent,
    ComplaintPageComponent,
    ParentChildrenPageComponent,
    ParentHomePageComponent,
    ParentNotificationComponent,
    VaccineSideEffectComponent,
    BookAppointmentComponent,
    UpdateAppointmentComponent,


  ]
})
export class ParentModule { }
