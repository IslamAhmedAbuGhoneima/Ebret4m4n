import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComplaintsComponent } from './Components/complaints/complaints.component';
import { SendReplyComponent } from './Components/send-reply/send-reply.component';
import { MinistryOfHealthHomePageComponent } from './Components/ministry-of-health-homepage/ministry-of-health-homepage.component';



@NgModule({
  declarations: [ComplaintsComponent,SendReplyComponent,MinistryOfHealthHomePageComponent],
  imports: [
    CommonModule
  ]
})
export class AdminOfMinistryOfHealthModule { }
