import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateScheduleComponent } from './Components/create-schedule/create-schedule.component';
import { DoctorHomePageComponent } from './Components/doctor-home-page/doctor-home-page.component';
import { MessagesComponent } from './Components/messages/messages.component';

@NgModule({
  declarations: [
    CreateScheduleComponent,
    DoctorHomePageComponent,
    MessagesComponent,
  ],
  imports: [CommonModule],
})
export class DoctorModule {}
