import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  Inject,
  OnInit,
  QueryList,
  ViewChildren,
  ViewEncapsulation,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { GlobalService } from '../../../../core/services/APIs/global.service';

@Component({
  selector: 'app-add-vaccines',
  imports: [CommonModule, FormsModule],
  templateUrl: './add-vaccines.component.html',
  styleUrl: './add-vaccines.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class AddVaccinesComponent implements OnInit {
  selectedVaccines: string[] = [];
  selectedStates: { [key: string]: boolean } = {};
  vaccines: any;
  ageInMonths: any;

  @ViewChildren('vaccineCheckbox') vaccineCheckboxes!: QueryList<ElementRef>;
  errorMessage: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _GlobalService: GlobalService,
    private dialog: MatDialogRef<AddVaccinesComponent>
  ) {}

  ngOnInit(): void {
    this.ageInMonths = this.calculateAgeInMonths(this.data);

    this._GlobalService.getVaccines().subscribe({
      next: (res) => {
        this.vaccines = this.formateData(
          this.filterVaccinesByAge(res.data, this.ageInMonths)
        );
      },
      error: (err) => {
        this.errorMessage = err.error.message;
      },
    });
  }

  calculateAgeInMonths(birthday: any): number {
    let day: number;
    let month: number;
    let year: number;

    if (Array.isArray(birthday)) {
      [day, month, year] = birthday;
      month = Number(month) - 1; // شهر 1 = index 0
    } else {
      throw new Error('قيمة تاريخ الميلاد غير صالحة');
    }

    const birthDate = new Date(year, month, day);
    const today = new Date();

    let totalMonths =
      (today.getFullYear() - birthDate.getFullYear()) * 12 +
      (today.getMonth() - birthDate.getMonth());

    if (today.getDate() < birthDate.getDate()) {
      totalMonths--;
    }

    return totalMonths >= 0 ? totalMonths : 0;
  }

  filterVaccinesByAge(vaccines: any[], ageInMonths: number): any[] {
    return vaccines.filter((vaccine) => {
      return ageInMonths >= vaccine.childAge;
    });
  }

  formateData(dataArray: any[]): any[] {
    if (!Array.isArray(dataArray)) {
      return [];
    }

    return dataArray.map((item) => {
      let updatedAge = item.childAge;

      if (item.name == 'الميلاد') {
        updatedAge = 'أول ٢٤ ساعة';
      } else if (item.name == 'الصفريه') {
        updatedAge = 'عند الميلاد';
      } else if (item.name == 'الدرن') {
        updatedAge = 'عند الميلاد';
      } else if (item.name == 'الاولي') {
        updatedAge = 'عمر شهرين';
      } else if (item.name == 'الثانيه') {
        updatedAge = 'عمر ٤ أشهر';
      } else if (item.name == 'الثالثه') {
        updatedAge = 'عمر ٦ أشهر';
      } else if (item.name == 'الرابعه') {
        updatedAge = 'عمر ٩ أشهر';
      } else if (item.name == 'الخامسه') {
        updatedAge = 'عمر سنة';
      } else if (item.name == 'المنشطه') {
        updatedAge = 'عمر سنة ونصف';
      }

      return {
        ...item,
        childAge: updatedAge,
      };
    });
  }

  submitVaccines() {
    this.selectedVaccines = [];

    this.vaccineCheckboxes.forEach((checkboxRef) => {
      const checkbox = checkboxRef.nativeElement as HTMLInputElement;
      if (checkbox.checked) {
        this.selectedVaccines.push(checkbox.value);
      }
    });

    this.dialog.close(this.selectedVaccines);
  }

  onCheckboxChange(event: any) {
    const value = event.target.value;
    if (event.target.checked) {
      this.selectedVaccines.push(value);
    } else {
      this.selectedVaccines = this.selectedVaccines.filter((v) => v !== value);
    }
  }

  close() {
    this.dialog.close();
  }
}
