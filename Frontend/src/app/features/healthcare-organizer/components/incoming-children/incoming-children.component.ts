import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrganizerService } from '../../services/organizer.service';

@Component({
  selector: 'app-incoming-children',
  standalone: false,
  templateUrl: './incoming-children.component.html',
  styleUrl: './incoming-children.component.css',
})
export class IncomingChildrenComponent implements OnInit {
  data: any;
  constructor(
    private router: Router,
    private _OrganizerService: OrganizerService
  ) {}
  ngOnInit(): void {
    this.children();
  }
  children() {
    this._OrganizerService.getChildren().subscribe({
      next: (res) => {
        this.data = res.data;
      },
      error: (err) => {},
    });
  }

  vaccinatedChildren() {
    this.router.navigate(['/healthcare-organizer/vaccinated-children']);
  }
}
