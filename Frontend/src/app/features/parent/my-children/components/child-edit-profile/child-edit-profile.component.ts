import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe, Location } from '@angular/common';
import { ParentService } from '../../../services/parent.service';
import { birthDateNotInFutureValidator } from '../../../../../core/customValidation/birthDateNotInFuture.validator';

@Component({
  selector: 'app-child-edit-profile',
  standalone: false,
  templateUrl: './child-edit-profile.component.html',
  styleUrl: './child-edit-profile.component.css',
})
export class ChildEditProfileComponent implements OnInit {
  formEditProfile!: FormGroup;
  msgError: string = '';
  data: any;
  userId: any;
  medicalImagesFromApi: any = [];
  today: Date = new Date();
  currentYear: number = this.today.getFullYear();
  months = [
    'يناير',
    'فبراير',
    'مارس',
    'أبريل',
    'مايو',
    'يونيو',
    'يوليو',
    'أغسطس',
    'سبتمبر',
    'أكتوبر',
    'نوفمبر',
    'ديسمبر',
  ];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private location: Location,
    private _ParentService: ParentService,
    private _ActivatedRoute: ActivatedRoute,
    private datePipe: DatePipe,
    private cdr: ChangeDetectorRef
  ) {}
  ngOnInit() {
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.userId = params.get('id');
      if (this.userId) {
        this.createForm();
        this.loadUserData();
      }
    });
  }

  createForm() {
    this.formEditProfile = this.fb.group({
      childName: [
        '',
        [
          Validators.required,
          Validators.pattern(/^[\u0600-\u06FF\s]+$/),
          Validators.minLength(3),
        ],
      ],
      NID: ['', [Validators.required, Validators.pattern(/^.{14}$/)]],
      gender: ['', [Validators.required]],
      weight: [
        '',
        [Validators.required, Validators.pattern(/^(1|[1-9]\d*)(\.\d+)?$/)],
      ],
      birthday: this.fb.group(
        {
          day: ['', [Validators.required]],
          month: ['', [Validators.required]],
          year: [
            '',
            [
              Validators.required,
              Validators.min(2025),
              Validators.max(this.currentYear),
              Validators.pattern(/^\d{4}$/),
            ],
          ],
        },
        { validators: birthDateNotInFutureValidator }
      ),
      medicalHistory: [''],
      medicalImages: this.fb.array([]),
    });
  }
  loadUserData() {
    this._ParentService.childDetails(this.userId).subscribe({
      next: (res) => {
        this.data = res.data;

        this.formEditProfile.patchValue({
          childName: this.data.name,
          NID: this.data.id,
          gender: this.data.gender,
          weight: this.data.weight,
          birthday: {
            day: this.datePipe.transform(this.data.birthDate, 'd'),
            month: this.datePipe.transform(this.data.birthDate, 'M'),
            year: this.datePipe.transform(this.data.birthDate, 'yyyy'),
          },
          medicalHistory: this.data.patientHistory,
        });

        this.medicalImagesFromApi = [];
        this.data.filePath.forEach((path: string) => {
          const fullUrl = 'http://localhost:5112' + path;
          const extension = path.split('.').pop()?.toLowerCase();
          const isImage = ['jpg', 'jpeg', 'png', 'gif', 'bmp', 'webp'].includes(
            extension || ''
          );

          const fileObj = isImage
            ? { preview: fullUrl, type: 'image', path: path }
            : {
                name: path.split('/').pop(),
                preview: fullUrl,
                type: 'file',
                path: path,
              };

          this.medicalImagesFromApi.push(fileObj);
        });

        // تحديث الـ FormArray للصور
        this.medicalImages.clear(); // مسح الصور القديمة من الفورم
        this.data.filePath.forEach(() => {
          this.medicalImages.push(new FormControl(null)); // إضافة قيمة فارغة للصور الجديدة
        });

        this.cdr.detectChanges(); // تحديث الفيو بعد تغيير الفورم
      },
      error: (err) => {
        this.msgError = err.error.message;
      },
    });
  }

  getImagePath(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    if (files && files.length > 0) {
      for (let i = 0; i < files.length; i++) {
        const file = files[i];
        this.medicalImages.push(this.fb.control(file)); // إضافة الملف إلى الـ FormArray

        const reader = new FileReader();
        reader.onload = (e: any) => {
          const fileType = file.type;

          if (fileType.startsWith('image/')) {
            this.medicalImagesFromApi.push({
              preview: e.target.result,
              type: 'image',
            });
          } else {
            this.medicalImagesFromApi.push({
              name: file.name,
              preview: '📄',
              type: 'file',
            });
          }
          this.cdr.detectChanges(); // تحديث العرض بعد إضافة الصورة
        };
        reader.readAsDataURL(file);
      }

      // تحديث حالة الفورم بعد إضافة صورة جديدة
      this.formEditProfile.markAsDirty();
      this.formEditProfile.updateValueAndValidity();
    }
  }

  removeImage(index: number) {
    const file = this.medicalImagesFromApi[index];

    if (file?.path) {
      this._ParentService.deleteChildFile(file.path).subscribe({
        next: (res) => {
          if (!res || res.success) {
            this._removeImageFromForm(index);
            this.formEditProfile.markAsDirty(); // تعيين النموذج كـ "معدل"
            this.formEditProfile.updateValueAndValidity(); // تحديث القيمة والتأكد من صحة الفورم
          } else {
            this.msgError = 'فشل حذف الملف من السيرفر.';
          }
        },
        error: (err) => {
          this.msgError = err?.error?.message || 'حدث خطأ أثناء الحذف.';
        },
      });
    } else {
      this._removeImageFromForm(index);
      this.formEditProfile.markAsDirty(); // تعيين النموذج كـ "معدل"
      this.formEditProfile.updateValueAndValidity(); // تحديث القيمة والتأكد من صحة الفورم
    }
  }

  private _removeImageFromForm(index: number) {
    this.medicalImages.removeAt(index);
    this.medicalImagesFromApi.splice(index, 1); // إزالة الصورة من المصفوفة

    this.formEditProfile.markAsDirty(); // علامة أن الفورم تغير
    this.formEditProfile.updateValueAndValidity(); // تحديث الفورم ليتحقق من التغييرات
    this.cdr.detectChanges();
  }

  formDataFormate(): FormData {
    const formData = new FormData();
    const formValue = this.formEditProfile.value;

    formData.append('Id', formValue.NID);
    formData.append('Name', formValue.childName);
    formData.append('Weight', formValue.weight);
    formData.append('Gender', formValue.gender);
    formData.append('PatientHistory', formValue.medicalHistory || '');

    const birthday = this.formEditProfile.value.birthday;
    if (birthday?.day && birthday?.month && birthday?.year) {
      const day = String(birthday.day).padStart(2, '0');
      const month = String(birthday.month).padStart(2, '0');
      const year = birthday.year;

      const formattedDate = `${year}-${month}-${day}`;

      formData.append('BirthDate', formattedDate);
    }

    const imageFiles = formValue.medicalImages;
    if (imageFiles && imageFiles.length > 0) {
      imageFiles.forEach((file: File) => {
        formData.append('ImageFiles', file);
      });
    }

    this.medicalImagesFromApi.forEach((file: any) => {
      if (file.path) {
        formData.append('ImagePaths', file.path);
      }
    });

    return formData;
  }

  saveNewData() {
    if (this.formEditProfile.valid && this.formEditProfile.dirty) {
      const model = this.formDataFormate();
   
      this._ParentService.childUpdate(this.userId, model).subscribe({
        next: (res) => {
          this.router.navigate(['/parent/my-children']);
        },
        error: (error) => {
          this.msgError = error.error.message;
        },
      });
    }
  }
  onDayInput(event: any): void {
    const value = event.target.valueAsNumber;

    if (!isNaN(value) && value >= 1 && value <= 31) {
      this.day?.setValue(value);
    } else {
      this.day?.setValue(null);
    }
  }

  goBack() {
    this.location.back();
  }

  get childName() {
    return this.formEditProfile.get('childName');
  }

  get id() {
    return this.formEditProfile.get('NID');
  }

  get gender() {
    return this.formEditProfile.get('gender');
  }

  get weight() {
    return this.formEditProfile.get('weight');
  }

  get birthday() {
    return this.formEditProfile.get('birthday') as FormGroup;
  }

  get day() {
    return this.formEditProfile.get('birthday.day');
  }

  get month() {
    return this.formEditProfile.get('birthday.month');
  }

  get year() {
    return this.formEditProfile.get('birthday.year');
  }

  get medicalHistory() {
    return this.formEditProfile.get('medicalHistory');
  }

  get medicalImages(): FormArray {
    return this.formEditProfile.get('medicalImages') as FormArray;
  }
}
