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
import { GovernorateAdminService } from '../../../../features/city-admin/services/governorateAdmin.service';
import { CityCenterService } from '../../../../features/city-centre-admin/services/cityCenter.service';
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
  egyptCityCenters: string[] = [
    'البدرشين',
    'العياط',
    'الصف',
    'أطفيح',
    'الواحات البحرية',
    'كرداسة',
    'أوسيم',
    'أبو النمرس',
    'منشأة القناطر',
    'المنيب',
    'الهرم',
    'الدقي',
    'بنها',
    'طوخ',
    'قها',
    'كفر شكر',
    'شبين القناطر',
    'الخانكة',
    'قليوب',
    'شبرا الخيمة',
    'المنتزه',
    'الرمل',
    'سيدي جابر',
    'محرم بك',
    'العجمي',
    'اللبان',
    'الجمرك',
    'دمنهور',
    'كفر الدوار',
    'رشيد',
    'إيتاي البارود',
    'أبو حمص',
    'الدلنجات',
    'المحمودية',
    'شبراخيت',
    'كوم حمادة',
    'حوش عيسى',
    'النوبارية',
    'المنصورة',
    'ميت غمر',
    'دكرنس',
    'بلقاس',
    'أجا',
    'منية النصر',
    'شربين',
    'تمي الأمديد',
    'كفر الشيخ',
    'دسوق',
    'فوه',
    'مطوبس',
    'سيدي سالم',
    'الحامول',
    'بلطيم',
    'قلين',
    'بيلا',
    'طنطا',
    'المحلة الكبرى',
    'زفتى',
    'كفر الزيات',
    'السنطة',
    'بسيون',
    'سمنود',
    'شبين الكوم',
    'منوف',
    'أشمون',
    'الباجور',
    'تلا',
    'بركة السبع',
    'السادات',
    'الزقازيق',
    'بلبيس',
    'أبو حماد',
    'أبو كبير',
    'ههيا',
    'فاقوس',
    'العاشر من رمضان',
    'منيا القمح',
    'الإبراهيمية',
    'ديرب نجم',
    'كفر صقر',
    'أولاد صقر',
    'مشتول السوق',
    'الفيوم',
    'سنورس',
    'إطسا',
    'طامية',
    'أبشواي',
    'يوسف الصديق',
    'المنيا',
    'أبوقرقاص',
    'ملوي',
    'دير مواس',
    'مطاي',
    'بني مزار',
    'سمالوط',
    'أسيوط',
    'ديروط',
    'منفلوط',
    'القوصية',
    'أبنوب',
    'أبو تيج',
    'الغنايم',
    'ساحل سليم',
    'البداري',
    'سوهاج',
    'أخميم',
    'المراغة',
    'البلينا',
    'جرجا',
    'دار السلام',
    'طهطا',
    'طما',
    'ساقلتة',
    'قنا',
    'أبوتشت',
    'نجع حمادي',
    'دشنا',
    'الوقف',
    'قوص',
    'نقادة',
    'فرشوط',
    'الأقصر',
    'الزينية',
    'البياضية',
    'القرنة',
    'أرمنت',
    'الطود',
    'أسوان',
    'دراو',
    'كوم أمبو',
    'نصر النوبة',
    'إدفو',
    'بني سويف',
    'الواسطى',
    'ناصر',
    'إهناسيا',
    'ببا',
    'سمسطا',
    'الفشن',
    'الخارجة',
    'الداخلة',
    'الفرافرة',
    'باريس',
    'بلاط',
    'مرسى مطروح',
    'النجيلة',
    'سيدي براني',
    'السلوم',
    'سيوة',
    'الضبعة',
    'العلمين',
    'الحمام',
    'العريش',
    'بئر العبد',
    'الشيخ زويد',
    'رفح',
    'الحسنة',
    'نخل',
    'الطور',
    'شرم الشيخ',
    'دهب',
    'نويبع',
    'طابا',
    'أبو رديس',
    'أبو زنيمة',
    'سانت كاترين',
    'الإسماعيلية',
    'القنطرة شرق',
    'القنطرة غرب',
    'فايد',
    'التل الكبير',
    'أبو صوير',
    'القصاصين',
    'السويس',
    'الجناين',
    'عتاقة',
    'فيصل',
    'بورسعيد',
    'الزهور',
    'الضواحي',
    'المناخ',
    'العرب',
    'الشرق',
    'الجنوب',
    'دمياط',
    'فارسكور',
    'كفر سعد',
    'الزرقا',
    'السرو',
    'كفر البطيخ',
    'عزبة البرج',
    'رأس البر',
    'الغردقة',
    'رأس غارب',
    'سفاجا',
    'القصير',
    'مرسى علم',
    'الشلاتين',
    'حلايب',
    'مصر الجديدة',
    'مدينة نصر',
    'المعادي',
    'حلوان',
    'عين شمس',
    'المرج',
    'المطرية',
    'الزيتون',
    'شبرا',
    'السيدة زينب',
    'بولاق',
    'الزمالك',
    'الدقي',
    'العجوزة',
    'الهرم',
    'فيصل',
    'العمرانية',
    'البساتين',
    'دار السلام',
    'المعصرة',
    'طرة',
    'المقطم',
    'الخليفة',
    'الجمالية',
    'الموسكي',
    'باب الشعرية',
    'الوايلي',
    'الظاهر',
    'الأزبكية',
    'عابدين',
    'قصر النيل',
  ];
  adminOfgovernorate: any;
  userId: any;
  data: any;
  msgError: any;
  role: any;
  errorMessage: any;
  healthUnits: any;
  cityAdminName: any;
  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location,
    private _ActivatedRoute: ActivatedRoute,
    private _HealthMinistryService: HealthMinistryService,
    private _AuthService: AuthService,
    private _GovernorateAdminService: GovernorateAdminService,
    private _CityCenterService: CityCenterService
  ) {}

  ngOnInit() {
    this.role = this._AuthService.getRole();
    this.adminOfgovernorate = this._AuthService.getUserGovernorate()!;
    this.cityAdminName = this._AuthService.getUserCity()!;
    if (this.role == 'cityAdmin') {
      this._AuthService
        .getHealthUnits(this.adminOfgovernorate, this.cityAdminName)
        .subscribe({
          next: (res) => {
            this.healthUnits = res.data;
          },
          error: (err) => {
            this.healthUnits = [];
          },
        });
    }
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
          Validators.pattern(/^[\u0600-\u06FF\s]+$/),
          Validators.minLength(3),
          Validators.maxLength(20),
        ],
      ],
      secondName: [
        '',
        [
          Validators.required,
          Validators.pattern(/^[\u0600-\u06FF\s]+$/),
          Validators.minLength(3),
          Validators.maxLength(20),
        ],
      ],
      email: ['', [Validators.required, Validators.email]],
      governorate: [''],
      city: [''],
      healthCareCenterId: [''],
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
    this.city?.clearValidators();

    switch (this.role) {
      case 'admin':
        this.governorate?.setValidators([Validators.required]);
        break;
      case 'governorateAdmin':
        this.city?.setValidators([Validators.required]);
        break;
      case 'cityAdmin':
        this.healthCareCenterId?.setValidators([Validators.required]);
        break;
    }

    this.governorate?.updateValueAndValidity();
    this.city?.updateValueAndValidity();
    this.healthCareCenterId?.updateValueAndValidity();
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
    } else if (this.role === 'governorateAdmin') {
      this._GovernorateAdminService.getAdminDetails(this.userId).subscribe({
        next: (res) => {
          this.data = res.data;
          const userData = {
            firstName: this.data.firstName,
            secondName: this.data.lastName,
            email: this.data.email,
            city: this.data.city,
          };

          this.formEditProfileAdmin.patchValue(userData);
        },
        error: (err) => {
          this.msgError = err.error.message;
        },
      });
    } else if (this.role === 'cityAdmin') {
      this._CityCenterService.getOrganizerDetails(this.userId).subscribe({
        next: (res) => {
          this.data = res.data;
          const userData = {
            firstName: this.data.firstName,
            secondName: this.data.lastName,
            email: this.data.email,
            healthCareCenterId: this.data?.hcCenterId,
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
    } else if (this.role == 'governorateAdmin') {
      const MODEL = {
        firstName: this.formEditProfileAdmin.value.firstName,
        lastName: this.formEditProfileAdmin.value.secondName,
        email: this.formEditProfileAdmin.value.email,
        city: this.formEditProfileAdmin.value.city,
      };
      this._GovernorateAdminService
        .editCityCenterAdmin(this.userId, MODEL)
        .subscribe({
          next: (res) => {
            this.route.navigate(['/admins']);
          },
          error: (err) => {
            this.errorMessage = err.error.Message;
          },
        });
    } else if (this.role == 'cityAdmin') {
      const MODEL = {
        firstName: this.formEditProfileAdmin.value.firstName,
        lastName: this.formEditProfileAdmin.value.secondName,
        email: this.formEditProfileAdmin.value.email,
        healthCareCenterId: this.healthCareCenterId?.value,
      };
      this._CityCenterService
        .editCityCenterOrganizer(this.userId, MODEL)
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

  get city() {
    return this.formEditProfileAdmin.get('city');
  }
  get governorate() {
    return this.formEditProfileAdmin.get('governorate');
  }
  get healthCareCenterId() {
    return this.formEditProfileAdmin.get('healthCareCenterId');
  }
  get email() {
    return this.formEditProfileAdmin.get('email');
  }

  goBack() {
    this.location.back();
  }
}
