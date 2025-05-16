import { Component, OnInit } from '@angular/core';
import { GovernorateAdminService } from '../../../services/governorateAdmin.service';

@Component({
  selector: 'app-centers-home-page',
  standalone: false,
  templateUrl: './centers-home-page.component.html',
  styleUrl: './centers-home-page.component.css',
})
export class CentersHomePageComponent implements OnInit {
  CitiesCentersList: string[] = [];
  errorMessage: any;
  constructor(private _GovernorateAdminService: GovernorateAdminService) {}
  ngOnInit(): void {
    this.getAllCities();
  }

  getAllCities() {
    this._GovernorateAdminService.getCitiesCenters().subscribe({
      next: (res) => {
        this.CitiesCentersList = res.data;
      },
      error: (error) => {
        this.errorMessage = error.error.Message;
      },
    });
  }
}
