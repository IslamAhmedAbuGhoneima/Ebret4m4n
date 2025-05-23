import { Component, OnInit } from '@angular/core';
import { CityCenterService } from '../../../services/cityCenter.service';

@Component({
  selector: 'app-units-home-page',
  standalone: false,
  templateUrl: './units-home-page.component.html',
  styleUrl: './units-home-page.component.css',
})
export class UnitsHomePageComponent implements OnInit {
  unitList: any[] = [];
  errorMessage: any;
  constructor(private _CityCenterService: CityCenterService) {}
  ngOnInit(): void {
    this.getAllCities();
  }

  getAllCities() {
    this._CityCenterService.getHealthCareUnits().subscribe({
      next: (res) => {
        this.unitList = res.data;
      },
      error: (error) => {
        this.errorMessage = error.error.Message;
      },
    });
  }
}
