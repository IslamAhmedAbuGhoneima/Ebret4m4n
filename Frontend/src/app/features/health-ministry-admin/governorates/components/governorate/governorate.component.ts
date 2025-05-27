import { Component, OnInit } from '@angular/core';
import { HealthMinistryService } from '../../../services/health-ministry.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-governorate',
  standalone: false,
  templateUrl: './governorate.component.html',
  styleUrl: './governorate.component.css',
})
export class GovernorateComponent implements OnInit {
  data: any;
  governorateName: string | undefined;

  constructor(
    private _ActivatedRoute: ActivatedRoute,
    private _HealthService: HealthMinistryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this._ActivatedRoute.paramMap.subscribe((params) => {
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
    this.router.navigate(['/health-ministry/governorates']);
  }
}
