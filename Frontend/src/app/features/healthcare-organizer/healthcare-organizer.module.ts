import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { RouterModule, Routes } from '@angular/router';
import { OrganizerOfHealthcareHomePageComponent } from './components/organizer-of-healthcare-home-page/organizer-of-healthcare-home-page.component';

const routes: Routes = [
  { path: '', redirectTo: '/healthcare-organizer/home', pathMatch: 'full' },
  { path: 'home', component: OrganizerOfHealthcareHomePageComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule, RouterModule.forChild(routes)
  ]
})
export class HeathCareOrganizerModule { }
