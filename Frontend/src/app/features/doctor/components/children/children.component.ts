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
}
