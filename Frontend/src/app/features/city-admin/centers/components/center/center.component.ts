import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-center',
  standalone: false,
  templateUrl: './center.component.html',
  styleUrl: './center.component.css',
})
export class CenterComponent implements OnInit {
  constructor(private location: Location) {}

  ngOnInit(): void {}
  goBack() {
    this.location.back();
  }
}
