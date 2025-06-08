import { Component, OnInit, ViewEncapsulation, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { passwordMatch } from '../../../../core/customValidation/passwordMatch.validator';
import { AuthService } from '../../services/auth.service';
import { Register } from '../../../../core/interfaces/register';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class RegisterComponent implements OnInit {
  firstFormGroup!: FormGroup;
  secondFormGroup!: FormGroup;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;
  errorMessage: any;
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
  healthUnits: any[] = [];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private _AuthService: AuthService,
    private _ToastrService: ToastrService
  ) {}

  ngOnInit() {
    this.createForm();
    this.secondFormGroup.get('city')?.valueChanges.subscribe(() => {
      this.tryLoadHealthUnits();
    });

    this.secondFormGroup.get('town')?.valueChanges.subscribe(() => {
      this.tryLoadHealthUnits();
    });
  }

  createForm() {
    this.firstFormGroup = this.fb.group(
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
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: passwordMatch }
    );
    this.secondFormGroup = this.fb.group({
      city: ['', [Validators.required]],
      town: ['', [Validators.required]],
      healthUnit: [''],
    });
  }

  register() {
    if (this.secondFormGroup.valid) {
      const model: Register = this.formatData();
      this._AuthService.signUp(model).subscribe({
        next: (res) => {
          Swal.fire({
            title: 'تم إنشاء حسابك بنجاح',
            text: 'يمكنك الآن تسجيل الدخول إلى حسابك.',
            icon: 'success',
            showCancelButton: false,
            confirmButtonColor: '#127453',
            cancelButtonColor: '#B4231B',
            confirmButtonText: 'تسجيل الدخول',
            allowOutsideClick: false,
          }).then((result) => {
            if (result.isConfirmed) {
              this.router.navigate(['/auth/login']);
            }
          });
        },
        error: (error) => {
          this.errorMessage = error.error.Message;
        },
      });
    } else {
      this.secondFormGroup.setErrors({ mismatch: true });
      this.secondFormGroup.markAllAsTouched();
    }
  }
  tryLoadHealthUnits() {
    const governorate = this.secondFormGroup.get('city')?.value;
    const city = this.secondFormGroup.get('town')?.value;

    if (governorate && city) {
      this._AuthService.getHealthUnits(governorate, city).subscribe({
        next: (res) => {
          this.healthUnits = res.data;
        },
        error: (err) => {
          console.error('خطأ في تحميل الوحدات الصحية:', err);
          this.healthUnits = [];
        },
      });
    }
  }

  get firstName() {
    return this.firstFormGroup.get('firstName');
  }
  get secondName() {
    return this.firstFormGroup.get('secondName');
  }

  get phone() {
    return this.firstFormGroup.get('phone');
  }
  get email() {
    return this.firstFormGroup.get('email');
  }

  get password() {
    return this.firstFormGroup.get('password');
  }

  get confirmPassword() {
    return this.firstFormGroup.get('confirmPassword');
  }

  get city() {
    return this.secondFormGroup.get('city');
  }
  get town() {
    return this.secondFormGroup.get('town');
  }
  get healthUnit() {
    return this.secondFormGroup.get('healthUnit');
  }
  togglePassword() {
    this.showPassword = !this.showPassword;
  }
  toggleConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  formatData(): Register {
    return {
      firstName: this.firstFormGroup.value.firstName,
      lastName: this.firstFormGroup.value.secondName,
      email: this.firstFormGroup.value.email,
      password: this.firstFormGroup.value.password,
      phoneNumber: this.firstFormGroup.value.phone,

      governorate: this.secondFormGroup.value.city,
      city: this.secondFormGroup.value.town,
      village: this.secondFormGroup.value.town,
      role: 'parent',
      healthCareCenterId: this.healthUnit?.value,
    };
  }
}
