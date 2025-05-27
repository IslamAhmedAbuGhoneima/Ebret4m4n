import { Component, OnInit } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CityCenterService } from '../../../../city-centre-admin/services/cityCenter.service';

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
      this.governorateName = params.get('governorateName')!;
      this.centerName = params.get('centerName')!;
      this.centerId = params.get('centerId')!;
      this.healthCareCenterName = params.get('healthCareCenterName')!;
      this.healthCareCenterId = params.get('healthCareCenterId')!;
      this.healthUnitDetails(this.healthCareCenterId);
    });
  }

  healthUnitDetails(HealthUnitId: any) {
    this._CityCenterService.getHealthCareUnitDetails(HealthUnitId).subscribe({
      next: (res) => {
        this.data = this.formateData(res.data);
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
    this.router.navigate([
      '/health-ministry/governorates',
      this.governorateName,
      this.centerName,
      this.centerId,
    ]);
  }
}
