import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ComplaintsListComponent } from './components/complaints-list/complaints-list.component';
import { ComplaintDetailsComponent } from './components/complaint-details/complaint-details.component';

const routes: Routes = [
  { path: '', component: ComplaintsListComponent },

  { path: 'complaint-details', component: ComplaintDetailsComponent },
];

@NgModule({
  declarations: [ComplaintsListComponent, ComplaintDetailsComponent],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class ComplaintsModule {}
