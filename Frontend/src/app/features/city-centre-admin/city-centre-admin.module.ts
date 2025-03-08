import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityCentreAdminHomePageComponent } from './components/city-centre-admin-home-page/city-centre-admin-home-page.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: '/city-centre-admin/home', pathMatch: 'full' },
  { path: 'home', component: CityCentreAdminHomePageComponent },
];
@NgModule({
  declarations: [CityCentreAdminHomePageComponent],
  imports: [CommonModule,RouterModule.forChild(routes)],
  exports: [],
})
export class CityCentreAdminModule { }
