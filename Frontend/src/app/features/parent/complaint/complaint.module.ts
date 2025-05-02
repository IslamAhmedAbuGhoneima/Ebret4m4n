import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReportComplaintComponent } from './components/report-complaint/report-complaint.component';
import { ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [{ path: '', component: ReportComplaintComponent }];

@NgModule({
  declarations: [ReportComplaintComponent],
  imports: [CommonModule, RouterModule.forChild(routes),ReactiveFormsModule],
})
export class ComplaintModule {}
