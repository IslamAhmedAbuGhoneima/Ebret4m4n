import { Component, OnInit } from '@angular/core';
import { HealthMinistryService } from '../../../services/health-ministry.service';

@Component({
  selector: 'app-governorates-list',
  standalone: false,
  templateUrl: './governorates-list.component.html',
  styleUrl: './governorates-list.component.css',
})
export class GovernoratesListComponent implements OnInit {
  governoratesList: string[] = [];
  errorMessage: any;
  constructor(private _HealthMinistryService: HealthMinistryService) {}
  ngOnInit(): void {
    this.getAllCities();
  }

  getAllCities() {
    this._HealthMinistryService.getGovernorates().subscribe({
      next: (res) => {
        this.governoratesList = res.data;
      },
      error: (error) => {
        this.errorMessage = error.error.Message;
      },
    });
  }
}
