import { Component, Inject, ViewEncapsulation } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatRadioModule } from '@angular/material/radio';
import { VaccinationEditComponent } from '../vaccination-edit/vaccination-edit.component';

@Component({
  selector: 'app-vaccination-booking',
  imports: [MatRadioModule],
  templateUrl: './vaccination-booking.component.html',
  styleUrl: './vaccination-booking.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class VaccinationBookingComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,

    private dialog: MatDialogRef<VaccinationEditComponent>
  ) {}

  close() {
    this.dialog.close();
  }
}
