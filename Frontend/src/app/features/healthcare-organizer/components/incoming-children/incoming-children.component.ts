import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-incoming-children',
  standalone: false,
  templateUrl: './incoming-children.component.html',
  styleUrl: './incoming-children.component.css',
})
export class IncomingChildrenComponent {
  constructor(private router:Router){}
  vaccinatedChildren(){

    this.router.navigate(['/healthcare-organizer/vaccinated-children']);
  }
}
