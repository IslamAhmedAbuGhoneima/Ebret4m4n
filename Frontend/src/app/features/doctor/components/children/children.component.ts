import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DoctorService } from '../../services/doctor.service';

@Component({
  selector: 'app-children',
  standalone: false,
  templateUrl: './children.component.html',
  styleUrl: './children.component.css',
})
export class ChildrenComponent implements OnInit {
  searchId: any;
  filteredData: any;
  data: any;

  constructor(private router: Router, private _DoctorService: DoctorService) {}
  ngOnInit(): void {
    this.getChildrenDisease();
  }

  getChildrenDisease() {
    this._DoctorService.childrenDisease().subscribe({
      next: (res) => {
        this.data = res.data;
        // this.filteredData = this.data;
        this.filteredData = [
          {
            id: '30309012404196',
            name: 'اسلام',
            ageInMonth: 2,
            birthDate: '2025-03-10T00:00:00',
            weight: 8,
            gender: 'm',
            '': null,
            vaccines: [],
            filePath: [
              '/Files/ChildReports/109f667d-3103-4640-b7bb-7f8d5c22c540.PNG',
              '/Files/ChildReports/ce47a2ab-8a81-4a85-88f6-4ac194e4650f.PNG',
            ],
          },
        ];
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
}
