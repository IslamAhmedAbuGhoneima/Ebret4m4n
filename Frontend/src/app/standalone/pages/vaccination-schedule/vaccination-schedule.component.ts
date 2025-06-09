import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-vaccination-schedule',
  imports: [],
  templateUrl: './vaccination-schedule.component.html',
  styleUrl: './vaccination-schedule.component.css',
})
export class VaccinationScheduleComponent {
  constructor(private router: Router) {}
  goBack() {
    this.router.navigate(['/home']);
  }
}
