import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddChildPageComponent } from './Components/add-child-page/add-child-page.component';
import { ChildHealthRecordComponent } from './Components/child-health-record/child-health-record.component';
import { ChildScheduleComponent } from './Components/child-schedule/child-schedule.component';
import { ComplaintPageComponent } from './Components/complaint-page/complaint-page.component';
import { ParentChildrenPageComponent } from './Components/parent-children-page/parent-children-page.component';
import { ParentHomePageComponent } from './Components/parent-home-page/parent-home-page.component';
import { ParentNotificationComponent } from './Components/parent-notification/parent-notification.component';
import { VaccineSideEffectComponent } from './Components/vaccine-side-effect/vaccine-side-effect.component';
import { BookAppointmentComponent } from './Components/book-appointment/book-appointment.component';
import { UpdateAppointmentComponent } from './Components/update-appointment/update-appointment.component';
import { DoctorCreateScheduleComponent } from './Components/doctor-create-schedule/create-schedule.component';
import { DoctorHomePageComponent } from './Components/doctor-home-page/doctor-home-page.component';
import { SharedModule } from '../../shared/shared.module';

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
    DoctorCreateScheduleComponent,
    DoctorHomePageComponent
  ],
  imports: [CommonModule, SharedModule],
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
    DoctorCreateScheduleComponent, DoctorHomePageComponent

  ]
})
export class UserModule { }
