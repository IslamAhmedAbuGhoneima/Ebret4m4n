import { Component, Inject, ViewEncapsulation } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatRadioModule } from '@angular/material/radio';

@Component({
  selector: 'app-vaccination-edit',
  imports: [MatRadioModule],
  templateUrl: './vaccination-edit.component.html',
  styleUrl: './vaccination-edit.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class VaccinationEditComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,

    private dialog: MatDialogRef<VaccinationEditComponent>
  ) {}

  close() {
    this.dialog.close();
  }
}
