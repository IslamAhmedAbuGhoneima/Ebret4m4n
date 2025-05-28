import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../../services/doctor.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-deferred-children',
  standalone: false,
  templateUrl: './deferred-children.component.html',
  styleUrl: './deferred-children.component.css',
})
export class DeferredChildrenComponent implements OnInit {
  searchId: any;
  filteredData: any;
  data: any;
  constructor(private router: Router, private _DoctorService: DoctorService) {}

  ngOnInit(): void {
    this.getChildrenSuspended();
  }

  getChildrenSuspended() {
    this._DoctorService.childrenSuspended().subscribe({
      next: (res) => {
        this.data = res.data;
        // this.filteredData = this.data;
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
