import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-health-unit',
  standalone: false,
  templateUrl: './health-unit.component.html',
  styleUrl: './health-unit.component.css',
})
export class HealthUnitComponent {
  constructor(private location: Location) {}
  goBack() {
    this.location.back();
  }
}
