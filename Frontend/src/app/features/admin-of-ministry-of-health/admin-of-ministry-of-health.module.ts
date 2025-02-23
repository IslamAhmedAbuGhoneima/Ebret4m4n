import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComplaintsComponent } from './Components/complaints/complaints.component';
import { SendReplyComponent } from './Components/send-reply/send-reply.component';
import { MinistryOfHealthHomePageComponent } from './Components/ministry-of-health-homepage/ministry-of-health-homepage.component';
import { SharedModule } from '../../shared/shared.module';



@NgModule({
  declarations: [ComplaintsComponent, SendReplyComponent, MinistryOfHealthHomePageComponent],
  imports: [
    CommonModule, SharedModule
  ]
})
export class AdminOfMinistryOfHealthModule { }
