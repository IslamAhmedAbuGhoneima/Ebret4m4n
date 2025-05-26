import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { IncomingChildrenComponent } from './components/incoming-children/incoming-children.component';
import { VaccinatedChildrenComponent } from './components/vaccinated-children/vaccinated-children.component';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
  { path: '', redirectTo: 'incoming-children', pathMatch: 'prefix' },
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
  imports: [CommonModule,FormsModule, RouterModule.forChild(routes)],
})
export class HeathCareOrganizerModule {}
