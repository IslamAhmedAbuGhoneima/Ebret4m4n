import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AddVaccinesComponent } from '../../../../../standalone/components/dialogs/add-vaccines/add-vaccines.component';
import { ParentService } from '../../../services/parent.service';
import { Router } from '@angular/router';
import { birthDateNotInFutureValidator } from '../../../../../core/customValidation/birthDateNotInFuture.validator';

@Component({
  selector: 'app-add-child',
  standalone: false,
  templateUrl: './add-child.component.html',
  styleUrl: './add-child.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class AddChildComponent implements OnInit {
  formAddChild!: FormGroup;
  showPassword: boolean = false;
  errorMessage: string = '';
  medicalImagesFiles: { preview: string; type: string; name?: string }[] = [];
  formValue: any = {};
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
    private matDialog: MatDialog,
    private _ParentService: ParentService,
    private router: Router
  ) {}
  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.formAddChild = this.fb.group({
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
      vaccines: this.fb.array([]),
    });
  }

  addChild() {
    if (this.formAddChild.valid) {
      const model = this.formDataFormate();

      this._ParentService.addChild(model).subscribe({
        next: (response) => {
          this.router.navigate(['/parent/my-children']);
        },
        error: (error) => {
          this.errorMessage = error.error.message;
        },
      });
    }
  }
  addVaccines() {
    (document.activeElement as HTMLElement)?.blur();
    setTimeout(() => {
      const dialogRef = this.matDialog.open(AddVaccinesComponent, {
        width: '690px',
        panelClass: 'dialog-add-vaccine-container',
        autoFocus: true,
        restoreFocus: false,
        disableClose: true,
        data: [this.day?.value, this.month?.value, this.year?.value],
      });

      dialogRef.afterClosed().subscribe((selectedVaccines: string[]) => {
        console.log(selectedVaccines);
        if (selectedVaccines) {
          const vaccinesFormArray = this.formAddChild.get(
            'vaccines'
          ) as FormArray;

          vaccinesFormArray.clear();

          selectedVaccines.forEach((vaccine) => {
            vaccinesFormArray.push(new FormControl(vaccine));
          });
        }
      });
    }, 0);
  }

  getImagePath(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    if (files && files.length > 0) {
      for (let i = 0; i < files.length; i++) {
        const file = files[i];

        this.medicalImages.push(new FormControl(file));

        const reader = new FileReader();
        reader.onload = (e: any) => {
          const fileType = file.type;

          if (fileType.startsWith('image/')) {
            this.medicalImagesFiles.push({
              preview: e.target.result,
              type: 'image',
            });
          } else {
            this.medicalImagesFiles.push({
              name: file.name,
              preview: '📄',
              type: 'file',
            });
          }
        };

        reader.readAsDataURL(file);
      }
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

  removeImage(index: number) {
    this.medicalImages.removeAt(index);
    this.medicalImagesFiles.splice(index, 1);
  }
  formDataFormate() {
    const formData = new FormData();
    const formValue = this.formAddChild.value;

    formData.append('Name', formValue.childName);
    formData.append('Id', formValue.NID);
    formData.append('Gender', formValue.gender);
    formData.append('Weight', formValue.weight);
    formData.append('PatientHistory', formValue.medicalHistory || '');

    const birthday = formValue.birthday;
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
        formData.append('ReportFiles', file);
      });
    }

    const vaccinesArray = Array.isArray(formValue.vaccines)
      ? formValue.vaccines.filter((v: string) => v && v.trim() !== '')
      : [];

    if (vaccinesArray.length > 0) {
      formData.append('TakedVaccines', JSON.stringify(vaccinesArray));
    }

    for (const [key, value] of formData.entries()) {
      if (key === 'TakedVaccines') {
        try {
          console.log(`${key}:`, JSON.parse(value as string));
        } catch {
          console.log(`${key}:`, value);
        }
      } else {
        console.log(`${key}:`, value);
      }
    }

    return formData;
  }

  get childName() {
    return this.formAddChild.get('childName');
  }
  get id() {
    return this.formAddChild.get('NID');
  }
  get gender() {
    return this.formAddChild.get('gender');
  }
  get weight() {
    return this.formAddChild.get('weight');
  }
  get birthday() {
    return this.formAddChild.get('birthday') as FormGroup;
  }
  get day() {
    return this.formAddChild.get('birthday.day');
  }

  get month() {
    return this.formAddChild.get('birthday.month');
  }

  get year() {
    return this.formAddChild.get('birthday.year');
  }
  get medicalHistory() {
    return this.formAddChild.get('medicalHistory');
  }
  get medicalImages(): FormArray {
    return this.formAddChild.get('medicalImages') as FormArray;
  }

  get vaccines(): FormArray {
    return this.formAddChild.get('vaccines') as FormArray;
  }
}
