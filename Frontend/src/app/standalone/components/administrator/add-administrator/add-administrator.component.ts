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
import { GovernorateAdminService } from '../../../../features/city-admin/services/governorateAdmin.service';
import { CityCenterService } from '../../../../features/city-centre-admin/services/cityCenter.service';

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
  egyptCityCenter: string[] = [
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
  healthUnits: any;
  adminOfgovernorate: any;
  cityAdminName: any;

  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location,
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
        city: [''],
        healthCareCenterId: [''],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: passwordMatch }
    );
    this.setValidatorsByUserType();
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
  addAdmin() {
    if (this.addAdminForm.valid) {
      console.log(this.addAdminForm.value);
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
    } else if (this.role === 'governorateAdmin') {
      const MODEL = {
        firstName: this.addAdminForm.value.firstName,
        lastName: this.addAdminForm.value.lastName,
        email: this.addAdminForm.value.email,
        password: this.addAdminForm.value.password,
        city: this.addAdminForm.value.city,
        governorate: this.adminOfgovernorate,
      };
      this._GovernorateAdminService.addCityCenterAdmin(MODEL).subscribe({
        next: (res) => {
          this.route.navigate(['/admins']);
        },
        error: (err) => {
          this.errorMessage = err.error.Message;
        },
      });
    } else if (this.role === 'cityAdmin') {
      const MODEL = {
        firstName: this.firstName?.value,
        lastName: this.lastName?.value,
        email: this.email?.value,
        password: this.password?.value,
        city: this.cityAdminName,
        governorate: this.adminOfgovernorate,
        healthCareCenterId: this.healthCareCenterId?.value,
        staffRole: 'organizer',
      };
      this._CityCenterService.addOrganizer(MODEL).subscribe({
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

  get city() {
    return this.addAdminForm.get('city');
  }
  get governorate() {
    return this.addAdminForm.get('governorate');
  }
  get healthCareCenterId() {
    return this.addAdminForm.get('healthCareCenterId');
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
