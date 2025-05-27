import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { AddHealthCare } from '../../../../../core/interfaces/addHealthCare';
import { CityCenterService } from '../../../services/cityCenter.service';

@Component({
  selector: 'app-add-unit',
  standalone: false,
  templateUrl: './add-unit.component.html',
  styleUrl: './add-unit.component.css',
})
export class AddUnitComponent implements OnInit {
  addUnitForm!: FormGroup;
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
  errorMessage: any;
  constructor(
    private fb: FormBuilder,
    private router: Router,

    private _CityCenterService: CityCenterService
  ) {}

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.addUnitForm = this.fb.group({
      healthUnitName: ['', [Validators.required]],

      LocationOfHealthUnit: this.fb.group({
        city: ['', [Validators.required]],
        center: ['', [Validators.required]],
        village: ['', [Validators.required]],
      }),

      WorkDays: this.fb.group({
        firstDay: ['', [Validators.required]],
        secondDay: ['', [Validators.required]],
      }),
    });
  }

  addUnit() {
    if (this.addUnitForm.valid) {
      if (this.addUnitForm.valid) {
        const model: AddHealthCare = {
          healthCareCenterName: this.healthUnitName?.value,
          firstDay: this.firstDay?.value,
          secondDay: this.secondDay?.value,
          governorate: this.city?.value,
          city: this.center?.value,
          village: this.village?.value,
        };

        this._CityCenterService.addHealthCareUnit(model).subscribe({
          next: (response) => {
            this.router.navigate(['/city-center-admin/units']);
          },
          error: (error) => {
            this.errorMessage = error.error.message;
          },
        });
      }
    }
  }

  get healthUnitName() {
    return this.addUnitForm.get('healthUnitName');
  }

  get LocationOfHealthUnit() {
    return this.addUnitForm.get('LocationOfHealthUnit');
  }

  get city() {
    return this.addUnitForm.get('LocationOfHealthUnit.city');
  }
  get center() {
    return this.addUnitForm.get('LocationOfHealthUnit.center');
  }
  get village() {
    return this.addUnitForm.get('LocationOfHealthUnit.village');
  }
  get workDays() {
    return this.addUnitForm.get('WorkDays');
  }
  get firstDay() {
    return this.addUnitForm.get('WorkDays.firstDay');
  }

  get secondDay() {
    return this.addUnitForm.get('WorkDays.secondDay');
  }

  goBack() {
    this.router.navigate(['/city-center-admin/units']);
  }
}
