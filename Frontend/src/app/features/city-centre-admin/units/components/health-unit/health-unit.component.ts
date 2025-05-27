import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddUnitComponent } from '../add-unit/add-unit.component';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CityCenterService } from '../../../services/cityCenter.service';

@Component({
  selector: 'app-health-unit',
  standalone: false,
  templateUrl: './health-unit.component.html',
  styleUrl: './health-unit.component.css',
})
export class HealthUnitComponent implements OnInit {
  governorateName: any;
  centerName: any;
  centerId: any;
  healthCareCenterName: any;
  healthCareCenterId: any;
  data: any;
  constructor(
    private router: Router,
    private _ActivatedRoute: ActivatedRoute,
    private _CityCenterService: CityCenterService
  ) {}
  ngOnInit(): void {
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.healthCareCenterName = params.get('healthCareCenterName')!;
      this.healthCareCenterId = params.get('healthCareCenterId')!;
      this.healthUnitDetails(this.healthCareCenterId);
    });
  }

  healthUnitDetails(HealthUnitId: any) {
    this._CityCenterService.getHealthCareUnitDetails(HealthUnitId).subscribe({
      next: (res) => {
        this.data = this.formateData(res.data);
        this.governorateName = this.data.governorate;
        this.centerName = this.data.city;
      },
      error: (err) => {},
    });
  }
  formateData(data: any): any {
    if (!data || typeof data !== 'object') {
      return {};
    }

    const dayMap: Record<string, string> = {
      Saturday: 'السبت',
      Sunday: 'الأحد',
      Monday: 'الإثنين',
      Tuesday: 'الثلاثاء',
      Wednesday: 'الأربعاء',
      Thursday: 'الخميس',
      Friday: 'الجمعة',
    };

    return {
      ...data,
      firstDayAr: dayMap[data.firstDay] || data.firstDay,
      secondDayAr: dayMap[data.secondDay] || data.secondDay,
    };
  }
  goBack() {
    this.router.navigate(['/city-center-admin/units']);
  }
}
