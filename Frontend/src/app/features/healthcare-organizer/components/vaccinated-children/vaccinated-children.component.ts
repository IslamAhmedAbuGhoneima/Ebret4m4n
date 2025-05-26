import { Component, OnInit } from '@angular/core';
import { OrganizerService } from '../../services/organizer.service';

@Component({
  selector: 'app-vaccinated-children',
  standalone: false,
  templateUrl: './vaccinated-children.component.html',
  styleUrl: './vaccinated-children.component.css',
})
export class VaccinatedChildrenComponent implements OnInit {
  vaccinatedList: any[] = [];
  searchId: string = '';
  filteredList: any[] = [];
  constructor(private _OrganizerService: OrganizerService) {}
  ngOnInit(): void {
    const storedData = localStorage.getItem('vaccinatedList');

    if (storedData) {
      this.vaccinatedList = JSON.parse(storedData);
      this.filteredList = this.vaccinatedList;
    }
  }
  onSearch() {
    const id = this.searchId.trim();
    if (id) {
      this.filteredList = this.vaccinatedList.filter((child: any) =>
        child.childId?.toString().includes(id)
      );
    } else {
      this.filteredList = this.vaccinatedList;
    }
  }
  done() {
    this._OrganizerService.updateVaccineStatues(this.vaccinatedList).subscribe({
      next: (res) => {
        localStorage.removeItem('vaccinatedList');
        this.vaccinatedList = [];
        this.filteredList = [];

      },
      error: (err) => {},
    });
  }
}
