import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { IncomingChildrenComponent } from './components/incoming-children/incoming-children.component';
import { VaccinatedChildrenComponent } from './components/vaccinated-children/vaccinated-children.component';

const routes: Routes = [
  { path: '', component: IncomingChildrenComponent },
  {
    path: 'incoming-children',
    component: IncomingChildrenComponent,
  },
  {
    path: 'vaccinated-children',
    component: VaccinatedChildrenComponent,
  },
];

@NgModule({
  declarations: [IncomingChildrenComponent, VaccinatedChildrenComponent],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class HeathCareOrganizerModule {}
