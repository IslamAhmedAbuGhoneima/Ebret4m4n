import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule, DatePipe, registerLocaleData } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AddChildComponent } from './components/add-child/add-child.component';
import { MyChildrenHomePageComponent } from './components/my-children-home-page/my-children-home-page.component';
import { MatButtonModule } from '@angular/material/button';
import { MatStepperModule } from '@angular/material/stepper';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatOption } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ChildEditProfileComponent } from './components/child-edit-profile/child-edit-profile.component';
import localeAr from '@angular/common/locales/ar';
import { ChildVaccinationScheduleComponent } from './components/child-vaccination-schedule/child-vaccination-schedule.component';

registerLocaleData(localeAr);

const routes: Routes = [
  { path: '', component: MyChildrenHomePageComponent },
  { path: 'add-child', component: AddChildComponent },
  {
    path: 'child-vaccine-schedule/:id',
    component: ChildVaccinationScheduleComponent,
  },
  {
    path: 'child-edit-profile/:id',
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
  providers: [{ provide: LOCALE_ID, useValue: 'ar' }, DatePipe],
})
export class MyChildrenModule {}
