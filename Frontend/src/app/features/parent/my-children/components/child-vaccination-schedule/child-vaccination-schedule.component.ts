import { Component, OnInit } from '@angular/core';
import { VaccinationEditComponent } from '../../../../../standalone/components/dialogs/vaccination-edit/vaccination-edit.component';
import { MatDialog } from '@angular/material/dialog';
import { VaccinationBookingComponent } from '../../../../../standalone/components/dialogs/vaccination-booking/vaccination-booking.component';
import { SideEffectsComponent } from '../../../../../standalone/components/dialogs/side-effects/side-effects.component';
import { ActivatedRoute } from '@angular/router';
import { ParentService } from '../../../services/parent.service';

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
  constructor(
    private matDialog: MatDialog,
    private _ActivatedRoute: ActivatedRoute,
    private _ParentService: ParentService
  ) {}
  ngOnInit(): void {
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.childId = params.get('id');
      this.childName = params.get('name');
    });
    this.getChildVaccines();
  }

  getChildVaccines() {
    this._ParentService.childVaccines().subscribe({
      next: (res) => {
        this.data = res.data;
        console.log(this.data);
      },
      error: (err) => {
        this.msgError = err.error.message;
      },
    });
  }

  editVaccine() {
    (document.activeElement as HTMLElement)?.blur();

    setTimeout(() => {
      const dialogRef = this.matDialog.open(VaccinationEditComponent, {
        width: '520px',
        panelClass: 'dialog-vaccination-Edit-container',
        autoFocus: true,
        restoreFocus: false,
        disableClose: true,
      });
    }, 0);
  }
  bookingVaccine() {
    (document.activeElement as HTMLElement)?.blur();

    setTimeout(() => {
      const dialogRef = this.matDialog.open(VaccinationBookingComponent, {
        width: '520px',
        panelClass: 'dialog-vaccination-booking-container',

        autoFocus: true,
        restoreFocus: false,
        disableClose: true,
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
}
