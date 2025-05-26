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
  filteredData: any[] = [];
  searchId: string = '';

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
        this.filteredData = this.data;
      },
      error: (err) => {},
    });
  }
  onSearch() {
    const id = this.searchId.trim();
    if (id) {
      this.filteredData = this.data.filter((child: any) =>
        child.childId?.toString().includes(id)
      );
    } else {
      this.filteredData = this.data;
    }
  }
  vaccinated(item: any) {
    const existing = JSON.parse(localStorage.getItem('vaccinatedList') || '[]');

    const isAlreadyAdded = existing.some(
      (child: any) => child.childId === item.childId
    );
    if (!isAlreadyAdded) {
      existing.push(item);
      localStorage.setItem('vaccinatedList', JSON.stringify(existing));
    }

    this.data = this.data.filter(
      (child: any) => child.childId !== item.childId
    );
    this.filteredData = this.filteredData.filter(
      (child: any) => child.childId !== item.childId
    );
  }

  postponeVaccination(appointmentId: any) {
    this._OrganizerService
      .postponeChildAppointmentVaccine(appointmentId)
      .subscribe({
        next: (res) => {
          this.children();
        },
        error: (err) => {},
      });
  }
}
