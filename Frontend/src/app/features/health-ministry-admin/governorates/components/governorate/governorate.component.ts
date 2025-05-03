import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { HealthMinistryService } from '../../../services/health-ministry.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-governorate',
  standalone: false,
  templateUrl: './governorate.component.html',
  styleUrl: './governorate.component.css',
})
export class GovernorateComponent {
  data: any;
  governorateName: string | undefined;

  constructor(
    private route: ActivatedRoute,
    private _HealthService: HealthMinistryService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.governorateName = params.get('governorateName')!;
      if (this.governorateName) this.GovernorateDetails();
    });
  }
  GovernorateDetails() {
    this._HealthService.getGovernorateDetails(this.governorateName!).subscribe({
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
