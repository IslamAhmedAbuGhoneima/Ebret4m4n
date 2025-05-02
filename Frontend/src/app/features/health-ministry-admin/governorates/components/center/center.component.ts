import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-center',
  standalone: false,
  templateUrl: './center.component.html',
  styleUrl: './center.component.css',
})
export class CenterComponent {
  constructor(private location: Location) {}
  goBack() {
    this.location.back();
  }
}
