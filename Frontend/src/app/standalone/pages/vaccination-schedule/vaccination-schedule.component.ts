import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-vaccination-schedule',
  imports: [],
  templateUrl: './vaccination-schedule.component.html',
  styleUrl: './vaccination-schedule.component.css',
})
export class VaccinationScheduleComponent {
  constructor(private location: Location) {}
  goBack() {
    this.location.back();
  }
}
