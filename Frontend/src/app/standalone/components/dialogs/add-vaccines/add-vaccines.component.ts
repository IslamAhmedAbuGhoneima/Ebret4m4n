import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  Inject,
  QueryList,
  ViewChildren,
  ViewEncapsulation,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-vaccines',
  imports: [CommonModule,FormsModule],
  templateUrl: './add-vaccines.component.html',
  styleUrl: './add-vaccines.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class AddVaccinesComponent {
  selectedVaccines: string[] = [];
  selectedStates: { [key: string]: boolean } = {};

  @ViewChildren('vaccineCheckbox') vaccineCheckboxes!: QueryList<ElementRef>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,

    private dialog: MatDialogRef<AddVaccinesComponent>
  ) {}

  close() {
    this.dialog.close();
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
}
