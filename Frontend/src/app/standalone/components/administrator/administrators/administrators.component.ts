import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-administrators',
  imports: [CommonModule,RouterLink],
  templateUrl: './administrators.component.html',
  styleUrl: './administrators.component.css',
})
export class AdministratorsComponent {}
