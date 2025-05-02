import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-governorate',
  standalone: false,
  templateUrl: './governorate.component.html',
  styleUrl: './governorate.component.css',
})
export class GovernorateComponent {
  constructor(private location: Location) {}
  goBack() {
    this.location.back();
  }
}
