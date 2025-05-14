import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule, Location } from '@angular/common';
import { passwordMatch } from '../../../../core/customValidation/passwordMatch.validator';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { HealthMinistryService } from '../../../../features/health-ministry-admin/services/health-ministry.service';

@Component({
  selector: 'app-add-administrator',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './add-administrator.component.html',
  styleUrl: './add-administrator.component.css',
})
export class AddAdministratorComponent implements OnInit {
  addAdminForm!: FormGroup;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;
  role: string | null | undefined;
  errorMessage: string = '';
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
  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location,
    private _HealthMinistryService: HealthMinistryService,
    private _AuthService: AuthService
  ) {}

  ngOnInit() {
    this.role = this._AuthService.getRole();
    this.createForm();
  }

  createForm() {
    this.addAdminForm = this.fb.group(
      {
        firstName: [
          '',
          [
            Validators.required,
            Validators.pattern(/^[\u0600-\u06FF\s]+$/), // يسمح فقط بالحروف العربية والمسافات
            Validators.minLength(3),
            Validators.maxLength(20),
          ],
        ],
        lastName: [
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
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: passwordMatch }
    );
    this.setValidatorsByUserType();
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
  addAdmin() {
    if (this.addAdminForm.valid) {
      this.getService();
    }
  }

  getService() {
    if (this.role === 'admin') {
      const MODEL = {
        firstName: this.addAdminForm.value.firstName,
        lastName: this.addAdminForm.value.lastName,
        email: this.addAdminForm.value.email,
        password: this.addAdminForm.value.password,
        governorate: this.addAdminForm.value.governorate,
      };
      this._HealthMinistryService.addGovernorateAdmin(MODEL).subscribe({
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
    return this.addAdminForm.get('firstName');
  }
  get lastName() {
    return this.addAdminForm.get('lastName');
  }

  get center() {
    return this.addAdminForm.get('center');
  }
  get governorate() {
    return this.addAdminForm.get('governorate');
  }

  get email() {
    return this.addAdminForm.get('email');
  }

  get password() {
    return this.addAdminForm.get('password');
  }

  get confirmPassword() {
    return this.addAdminForm.get('confirmPassword');
  }
  togglePassword() {
    this.showPassword = !this.showPassword;
  }
  toggleConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  goBack() {
    this.location.back();
  }
}
