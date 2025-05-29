import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  RouterLink,
  RouterLinkActive,
  RouterModule,
  RouterOutlet,
  Routes,
} from '@angular/router';
import { DeferredChildrenComponent } from './components/deferred-children/deferred-children.component';
import { ChildrenComponent } from './components/children/children.component';
import { DoctorChildDetailsComponent } from './components/doctor-child-details/doctor-child-details.component';
import { FormsModule } from '@angular/forms';
const routes: Routes = [
  { path: '', redirectTo: 'children', pathMatch: 'full' },
  { path: 'children', component: ChildrenComponent },
  { path: 'deferred-children', component: DeferredChildrenComponent },
  {
    path: 'doctor-child-details/:id',
    component: DoctorChildDetailsComponent,
  },

  {
    path: 'deferred-children/doctor-child-details/:id',
    component: DoctorChildDetailsComponent,
  },
  {
    path: 'children/doctor-child-details/:id',
    component: DoctorChildDetailsComponent,
  },
];

@NgModule({
  declarations: [
    ChildrenComponent,
    DeferredChildrenComponent,
    DoctorChildDetailsComponent,
  ],
  imports: [CommonModule, FormsModule, RouterModule.forChild(routes)],
  exports: [],
})
export class DoctorModule {}
