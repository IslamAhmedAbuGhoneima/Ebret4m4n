import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ParentService } from '../../../services/parent.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-parent-profile-edit',
  standalone: false,
  templateUrl: './parent-profile-edit.component.html',
  styleUrl: './parent-profile-edit.component.css',
})
export class ParentProfileEditComponent implements OnInit {
  formEditProfile!: FormGroup;
  userId: any;
  data: any;
  msgError: any;
  healthcareDetails: any;
  heathCareId: any;
  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location,
    private _ParentService: ParentService,
    private _ActivatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.createForm();
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.userId = params.get('id');
    });
    this.loadUserData();
  }

  createForm() {
    this.formEditProfile = this.fb.group({
      firstName: [
        '',
        [
          Validators.required,
          Validators.pattern(/^[\u0600-\u06FF\s]+$/), // يسمح فقط بالحروف العربية والمسافات
          Validators.minLength(3),
          Validators.maxLength(20),
        ],
      ],
      secondName: [
        '',
        [
          Validators.required,
          Validators.pattern(/^[\u0600-\u06FF\s]+$/), // يسمح فقط بالحروف العربية والمسافات
          Validators.minLength(3),
          Validators.maxLength(20),
        ],
      ],
      phone: ['', [Validators.required, Validators.minLength(11)]],
      email: ['', [Validators.required, Validators.email]],
      city: ['', [Validators.required]],
      town: ['', [Validators.required]],
      healthUnit: ['', [Validators.required]],
    });
  }
  loadUserData() {
    forkJoin({
      profile: this._ParentService.parentProfile(this.userId),
      healthcare: this._ParentService.ParentHealthcareDetails(),
    }).subscribe({
      next: (res) => {
        this.data = res.profile.data;
        this.healthcareDetails = res.healthcare.data;
        this.heathCareId = this.data.healthCareCenterId;
        const model = {
          firstName: this.data?.firstName,
          secondName: this.data?.lastName,
          email: this.data?.email,
          phone: this.data?.phoneNumber,
          governorate: this.data?.governorate,
          city: this.data?.city,
          town: this.data?.village,
          healthUnit: this.data?.healthCareCenterId,
        };
        this.formEditProfile.patchValue(model);
      },
      error: (err) => {
        this.msgError = err.error.message;
      },
    });
  }

  saveNewData() {
    if (this.formEditProfile.valid && this.formEditProfile.dirty) {
      const model = {
        firstName: this.firstName?.value,
        lastName: this.secondName?.value,
        phoneNumber: this.phone?.value,
        governorate: this.city?.value,
        city: this.town?.value,
        village: this.town?.value,
        healthCareCenterId: this.heathCareId,
      };
      this._ParentService.updateParentProfile(model).subscribe({
        next: (res) => {
          this.route.navigate(['/parent/dashboard/user-profile', this.userId]);
        },
        error: (err) => {
          this.msgError = err.error.message;
        },
      });
    }
  }
  goBack() {
    this.location.back();
  }
  get firstName() {
    return this.formEditProfile.get('firstName');
  }
  get secondName() {
    return this.formEditProfile.get('secondName');
  }

  get phone() {
    return this.formEditProfile.get('phone');
  }
  get email() {
    return this.formEditProfile.get('email');
  }

  get city() {
    return this.formEditProfile.get('city');
  }
  get town() {
    return this.formEditProfile.get('town');
  }
  get healthUnit() {
    return this.formEditProfile.get('healthUnit');
  }
}
