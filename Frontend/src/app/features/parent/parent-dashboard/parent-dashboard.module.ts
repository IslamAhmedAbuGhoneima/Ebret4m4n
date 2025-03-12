import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ParentHomePageComponent } from './components/parent-home-page/parent-home-page.component';


const routes: Routes = [{ path: '', component: ParentHomePageComponent },
];


@NgModule({
  declarations: [ParentHomePageComponent],
  imports: [
    CommonModule, RouterModule.forChild(routes)
  ]
})
export class ParentDashboardModule { }
