import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { GovernorateAdminService } from '../../../services/governorateAdmin.service';
import { AuthService } from '../../../../auth/services/auth.service';

@Component({
  selector: 'app-center',
  standalone: false,
  templateUrl: './center.component.html',
  styleUrl: './center.component.css',
})
export class CenterComponent implements OnInit {
  data: any;
  cityName: string | undefined;
  governorate: any;
  constructor(
    private route: ActivatedRoute,
    private _GovernorateAdminService: GovernorateAdminService,
    private location: Location,
    private _AuthService: AuthService
  ) {}

  ngOnInit(): void {
    this.governorate = this._AuthService.getUserGovernorate();
    this.route.paramMap.subscribe((params) => {
      this.cityName = params.get('cityName')!;
      if (this.cityName) this.GovernorateDetails();
    });
  }
  GovernorateDetails() {
    this._GovernorateAdminService
      .getCityCenterDetails(this.cityName)
      .subscribe({
        next: (res) => {
          this.data = res.data;
        },
        error: (err) => {
          console.error('حدث خطأ', err);
        },
      });
  }

  goBack() {
    this.location.back();
  }
}
