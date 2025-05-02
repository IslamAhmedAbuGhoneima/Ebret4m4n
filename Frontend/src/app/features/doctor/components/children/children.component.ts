import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-children',
  standalone: false,
  templateUrl: './children.component.html',
  styleUrl: './children.component.css',
})
export class ChildrenComponent {
  constructor(private router: Router) {}
  search(e: any) {
    console.log(e.target.value);
  }
  // goToChildDetails() {
  //   this.router.navigate(['/doctor/doctor-child-details']);
  // }
}
