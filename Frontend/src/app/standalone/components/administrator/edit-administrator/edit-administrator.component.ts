import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule, Location } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HealthMinistryService } from '../../../../features/health-ministry-admin/services/health-ministry.service';
import { AuthService } from '../../../../features/auth/services/auth.service';
@Component({
  selector: 'app-edit-admin',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-administrator.component.html',
  styleUrl: './edit-administrator.component.css',
})
export class EditAdministratorComponent implements OnInit {
  formEditProfileAdmin!: FormGroup;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;
  egyptGovernorates: string[] = [
    'القاهرة',
    'الجيزة',
    'الإسكندرية',
    'الدقهلية',
    'البحر الأحمر',
    'البحيرة',
    'الفيوم',
    'الغربية',
    'الإسماعيلية',
    'المنوفية',
    'المنيا',
    'القليوبية',
    'الوادي الجديد',
    'السويس',
    'أسوان',
    'أسيوط',
    'بني سويف',
    'بورسعيد',
    'دمياط',
    'جنوب سيناء',
    'كفر الشيخ',
    'مطروح',
    'الأقصر',
    'قنا',
    'شمال سيناء',
    'سوهاج',
    'الشرقية',
  ];
  userId: any;
  data: any;
  msgError: any;
  role: any;
  errorMessage: any;
  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location,
    private _ActivatedRoute: ActivatedRoute,
    private _HealthMinistryService: HealthMinistryService,
    private _AuthService: AuthService
  ) {}

  ngOnInit() {
    this.role = this._AuthService.getRole();
    this.createForm();
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.userId = params.get('userId');
    });
    this.loadUserData();
  }

  createForm() {
    this.formEditProfileAdmin = this.fb.group({
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
      email: ['', [Validators.required, Validators.email]],
      governorate: [''],
      center: [''],
    });
    this.setValidatorsByUserType();
  }

  saveNewData() {
    if (this.formEditProfileAdmin.valid && this.formEditProfileAdmin.dirty) {
      this.getService();
    }
  }
  setValidatorsByUserType() {
    this.governorate?.clearValidators();
    this.center?.clearValidators();

    switch (this.role) {
      case 'admin':
        this.governorate?.setValidators([Validators.required]);
        break;
      case 'center-admin':
        this.center?.setValidators([Validators.required]);
        break;
    }

    this.governorate?.updateValueAndValidity();
    this.center?.updateValueAndValidity();
  }

  loadUserData() {
    if (this.role === 'admin') {
      this._HealthMinistryService
        .getGovernorateAdminDetails(this.userId)
        .subscribe({
          next: (res) => {
            this.data = res.data;
            const userData = {
              firstName: this.data.firstName,
              secondName: this.data.lastName,
              email: this.data.email,
              governorate: this.data.governorate,
            };

            this.formEditProfileAdmin.patchValue(userData);
          },
          error: (err) => {
            this.msgError = err.error.message;
          },
        });
    }
  }

  getService() {
    if (this.role === 'admin') {
      const MODEL = {
        firstName: this.formEditProfileAdmin.value.firstName,
        lastName: this.formEditProfileAdmin.value.secondName,
        email: this.formEditProfileAdmin.value.email,
        password: this.formEditProfileAdmin.value.password,
        governorate: this.formEditProfileAdmin.value.governorate,
      };
      this._HealthMinistryService
        .editGovernorateAdmin(this.userId, MODEL)
        .subscribe({
          next: (res) => {
            this.route.navigate(['/admins']);
          },
          error: (err) => {
            this.errorMessage = err.error.Message;
          },
        });
    }
  }
  get firstName() {
    return this.formEditProfileAdmin.get('firstName');
  }
  get secondName() {
    return this.formEditProfileAdmin.get('secondName');
  }

  get center() {
    return this.formEditProfileAdmin.get('center');
  }
  get governorate() {
    return this.formEditProfileAdmin.get('governorate');
  }
  get email() {
    return this.formEditProfileAdmin.get('email');
  }

  goBack() {
    this.location.back();
  }
}
