import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ParentHomePageComponent } from './components/parent-home-page/parent-home-page.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog'; // <-- أضف هذا السطر
import { ParentProfileEditComponent } from './components/parent-profile-edit/parent-profile-edit.component';
import { ParentProfileComponent } from './components/parent-profile/parent-profile.component';
import { ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  { path: '', component: ParentHomePageComponent },
  { path: 'edit-user-profile/:id', component: ParentProfileEditComponent },
  { path: 'user-profile/:id', component: ParentProfileComponent },
];

@NgModule({
  declarations: [
    ParentHomePageComponent,
    ParentProfileEditComponent,
    ParentProfileComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    MatButtonModule,
    MatDialogModule,
  ],
})
export class ParentDashboardModule {}
