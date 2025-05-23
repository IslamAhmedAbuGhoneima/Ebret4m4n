import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { CityCenterService } from '../../../services/cityCenter.service';
import { AuthService } from '../../../../auth/services/auth.service';

@Component({
  selector: 'app-complaint-details',
  standalone: false,
  templateUrl: './complaint-details.component.html',
  styleUrl: './complaint-details.component.css',
})
export class ComplaintDetailsComponent implements OnInit {
  complaintId: any;
  data: any;
  organizerGovernorateName: any;
  organizerCityName: any;
  organizerEmail: any;
  complaintHandled: boolean = false;

  constructor(
    private _ActivatedRoute: ActivatedRoute,
    private location: Location,
    private _CityCenterService: CityCenterService,
    private _AuthService: AuthService
  ) {}

  ngOnInit(): void {
    this.organizerGovernorateName = this._AuthService.getUserGovernorate()!;
    this.organizerCityName = this._AuthService.getUserCity()!;
    this.organizerEmail = this._AuthService.getUserEmail()!;
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.complaintId = params.get('id');
      this.getComplaintDetails();
    });
  }
  getComplaintDetails() {
    this._CityCenterService.complaintDetails(this.complaintId).subscribe({
      next: (res) => {
        this.data = res.data;
      },
      error: (err) => {},
    });
  }
  handleComplaint() {
    this._CityCenterService.handleComplaint(this.complaintId).subscribe({
      next: (res) => {
        this.data = res.data;
        this.complaintHandled = true;
      },
      error: (err) => {},
    });
  }
  goBack() {
    this.location.back();
  }
}
