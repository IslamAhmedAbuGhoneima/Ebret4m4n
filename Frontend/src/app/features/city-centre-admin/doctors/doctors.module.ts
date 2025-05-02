import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditDoctorComponent } from './components/edit-doctor/edit-doctor.component';
import { AddDoctorComponent } from './components/add-doctor/add-doctor.component';
import { DoctorsHomePageComponent } from './components/doctors-home-page/doctors-home-page.component';
import { RouterModule, Routes } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  { path: '', component: DoctorsHomePageComponent },

  { path: 'add-doctor', component: AddDoctorComponent },
  {
    path: 'edit-doctor',
    component: EditDoctorComponent,
  },
];

@NgModule({
  declarations: [
    EditDoctorComponent,
    AddDoctorComponent,
    DoctorsHomePageComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatDialogModule,
    ReactiveFormsModule,
  ],
})
export class DoctorsModule {}
