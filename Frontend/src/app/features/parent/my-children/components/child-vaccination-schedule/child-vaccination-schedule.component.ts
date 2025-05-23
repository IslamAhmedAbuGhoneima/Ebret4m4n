import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { VaccinationBookingComponent } from '../../../../../standalone/components/dialogs/vaccination-booking/vaccination-booking.component';
import { SideEffectsComponent } from '../../../../../standalone/components/dialogs/side-effects/side-effects.component';
import { ActivatedRoute } from '@angular/router';
import { ParentService } from '../../../services/parent.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-child-vaccination-schedule',
  standalone: false,
  templateUrl: './child-vaccination-schedule.component.html',
  styleUrl: './child-vaccination-schedule.component.css',
})
export class ChildVaccinationScheduleComponent implements OnInit {
  childId: any;
  childName: any;
  msgError: any;
  data: any;
  vaccines: any;
  ageInMonth: any;
  bookingOrNot: boolean = false;
  appointmentId: any;
  allVaccinesTaken: boolean = false;
  timeTakenVaccine: any;
  constructor(
    private matDialog: MatDialog,
    private _ActivatedRoute: ActivatedRoute,
    private _ParentService: ParentService,
    private location: Location
  ) {}
  ngOnInit(): void {
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.childId = params.get('id');
    });
    this.getChildVaccines();
  }

  getChildVaccines() {
    this._ParentService.childVaccineSchedule(this.childId).subscribe({
      next: (res) => {
        this.data = res.data;
        this.childName = this.data.name;
        this.vaccines = this.formateData(this.data.vaccines);
        this.ageInMonth = this.data.ageInMonth;
        this.checkAllVaccinesTaken();
        this.vaccines.forEach((vaccine: any) => {
          if (this.ageInMonth === vaccine.childAge) {
            this._ParentService
              .appointmentExists(this.data.id, vaccine.name)
              .subscribe({
                next: (res) => {
                  this.bookingOrNot = true;
                  this.appointmentId = res.data.appointmentId;
                },
                error: (error) => {},
              });
          }
        });
      },
      error: (err) => {
        this.msgError = err.error.message;
      },
    });
  }

  getCurrentVaccineName(vaccineId: any): string | null {
    if (!this.vaccines || this.vaccines.length === 0) return null;

    const currentVaccine = this.vaccines.find((vaccine: any) => {
      return vaccine.id === vaccineId;
    });

    return currentVaccine?.name || null;
  }
  checkAllVaccinesTaken() {
    this.allVaccinesTaken = this.vaccines.every(
      (vaccine: any) => vaccine.isTaken
    );
  }

  bookingVaccine(vaccineId: any) {
    (document.activeElement as HTMLElement)?.blur();

    setTimeout(() => {
      const dialogRef = this.matDialog.open(VaccinationBookingComponent, {
        width: '520px',
        panelClass: 'dialog-vaccination-booking-container',
        autoFocus: true,
        restoreFocus: false,
        disableClose: true,
        data: {
          childId: this.childId,
          vaccineName: this.getCurrentVaccineName(vaccineId),
          bookingExists: this.bookingOrNot,
          appointmentId: this.appointmentId,
        },
      });
    }, 0);
  }
  expectedSideEffects() {
    (document.activeElement as HTMLElement)?.blur();

    setTimeout(() => {
      const dialogRef = this.matDialog.open(SideEffectsComponent, {
        width: '350px',
        panelClass: 'dialog-side-effects-container',
        autoFocus: true,
        restoreFocus: false,
        disableClose: true,
      });
    }, 0);
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
        updatedAge = 'شهرين';
      } else if (item.name == 'الثانيه') {
        updatedAge = '٤ أشهر';
      } else if (item.name == 'الثالثه') {
        updatedAge = '٦ أشهر';
      } else if (item.name == 'الرابعه') {
        updatedAge = '٩ أشهر';
      } else if (item.name == 'الخامسه') {
        updatedAge = 'سنة';
      } else if (item.name == 'المنشطه') {
        updatedAge = 'سنة ونصف';
      }

      return {
        ...item,
        childAgeDetails: updatedAge,
      };
    });
  }
  getDaysDifference(takeAt: string): number {
    const now = new Date();
    const targetDate = new Date(takeAt);

    const after48Hours = new Date(targetDate.getTime() + 48 * 60 * 60 * 1000);

    const diffInMs = after48Hours.getTime() - now.getTime();

    const diffInHours = Math.floor(diffInMs / (1000 * 60 * 60));

    return diffInHours;
  }
  goBack() {
    this.location.back();
  }
}
