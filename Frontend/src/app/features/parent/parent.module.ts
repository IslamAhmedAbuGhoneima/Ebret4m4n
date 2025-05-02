import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./parent-dashboard/parent-dashboard.module').then(
        (m) => m.ParentDashboardModule
      ),
  },
  {
    path: 'my-children',
    loadChildren: () =>
      import('./my-children/my-children.module').then(
        (m) => m.MyChildrenModule
      ),
  },
  {
    path: 'complaint',
    loadChildren: () =>
      import('./complaint/complaint.module').then((m) => m.ComplaintModule),
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  // #####important##### exports to use this module in doctor module
  exports: [],
})
export class ParentModule {}
