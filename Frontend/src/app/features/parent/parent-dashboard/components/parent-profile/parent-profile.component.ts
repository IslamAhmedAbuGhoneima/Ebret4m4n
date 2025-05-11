import { Component, OnInit } from '@angular/core';
import { ParentService } from '../../../services/parent.service';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-parent-profile',
  standalone: false,
  templateUrl: './parent-profile.component.html',
  styleUrl: './parent-profile.component.css',
})
export class ParentProfileComponent implements OnInit {
  data: any;
  userId: any;
  msgError: any;
  healthcareDetails: any;
  constructor(
    private _ParentService: ParentService,
    private _ActivatedRoute: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.userId = params.get('id');
    });

    forkJoin({
      profile: this._ParentService.parentProfile(this.userId),
      healthcare: this._ParentService.ParentHealthcareDetails(),
    }).subscribe({
      next: (res) => {
        this.data = res.profile.data;
        this.healthcareDetails = res.healthcare.data;
      },
      error: (err) => {
        this.msgError = err.error.message;
      },
    });
  }
}
