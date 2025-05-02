import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-side-effects',
  imports: [],
  templateUrl: './side-effects.component.html',
  styleUrl: './side-effects.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class SideEffectsComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,

    private dialog: MatDialogRef<SideEffectsComponent>
  ) {}

  close() {
    this.dialog.close();
  }
}
