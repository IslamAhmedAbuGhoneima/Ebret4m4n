import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AddChildComponent } from './components/add-child/add-child.component';
import { MyChildrenHomePageComponent } from './components/my-children-home-page/my-children-home-page.component';
import { MatButtonModule } from '@angular/material/button';
import { MatStepperModule } from '@angular/material/stepper';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatOption } from '@angular/material/core';
import { ChildVaccinationScheduleComponent } from './components/child-vaccination-schedule/child-vaccination-schedule.component';
import { MatDialogModule } from '@angular/material/dialog';
import { SideEffectsComponent } from '../../../standalone/components/dialogs/side-effects/side-effects.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ChildEditProfileComponent } from './components/child-edit-profile/child-edit-profile.component';

const routes: Routes = [
  { path: '', component: MyChildrenHomePageComponent },
  { path: 'add-child', component: AddChildComponent },
  {
    path: 'child-vaccine-schedule',
    component: ChildVaccinationScheduleComponent,
  },
  {
    path: 'child-edit-profile',
    component: ChildEditProfileComponent,
  },
];

@NgModule({
  declarations: [
    AddChildComponent,
    MyChildrenHomePageComponent,
    ChildVaccinationScheduleComponent,
    ChildEditProfileComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatButtonModule,
    MatStepperModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatOption,
    ReactiveFormsModule,
    MatDialogModule,
    MatTooltipModule,
  ],
})
export class MyChildrenModule {}
