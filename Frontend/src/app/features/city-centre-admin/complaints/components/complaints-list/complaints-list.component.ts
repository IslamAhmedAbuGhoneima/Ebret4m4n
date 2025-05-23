import { Component, OnInit } from '@angular/core';
import { CityCenterService } from '../../../services/cityCenter.service';

@Component({
  selector: 'app-complaints-list',
  standalone: false,
  templateUrl: './complaints-list.component.html',
  styleUrl: './complaints-list.component.css',
})
export class ComplaintsListComponent implements OnInit {
  complaintsList: any[] = [];
  errorMessage: any;
  constructor(private _CityCenterService: CityCenterService) {}
  ngOnInit(): void {
    this.getAllCities();
  }

  getAllCities() {
    this._CityCenterService.getComplaints().subscribe({
      next: (res) => {
        this.complaintsList = res.data;
      },
      error: (error) => {
        this.errorMessage = error.error.Message;
      },
    });
  }
}
