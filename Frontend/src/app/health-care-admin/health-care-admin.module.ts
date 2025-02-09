import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HealthCareAdminHomePageComponent } from './Components/health-care-admin-home-page/health-care-admin-home-page.component';
import { VaccineInventoryComponent } from './Components/vaccine-inventory/vaccine-inventory.component';



@NgModule({
  declarations: [HealthCareAdminHomePageComponent,VaccineInventoryComponent],
  imports: [
    CommonModule
  ]
})
export class HealthCareAdminModule { }
