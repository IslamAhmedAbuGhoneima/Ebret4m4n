import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { vaccineSideEffects } from '../../../../core/interfaces/sideEffectData';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-side-effects',
  imports: [CommonModule],
  templateUrl: './side-effects.component.html',
  styleUrl: './side-effects.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class SideEffectsComponent implements OnInit {
  effects: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog: MatDialogRef<SideEffectsComponent>
  ) {}
  ngOnInit(): void {
    const vaccineKey = this.data;
    this.effects =
      vaccineSideEffects[vaccineKey] ?? vaccineSideEffects['أي لقاح آخر'];
  }

  close() {
    this.dialog.close();
  }
}
