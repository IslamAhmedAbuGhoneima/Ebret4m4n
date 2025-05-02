import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddUnitComponent } from '../add-unit/add-unit.component';
import { Location } from '@angular/common';

@Component({
  selector: 'app-health-unit',
  standalone: false,
  templateUrl: './health-unit.component.html',
  styleUrl: './health-unit.component.css',
})
export class HealthUnitComponent {
  constructor(private matDialog: MatDialog,private location:Location) {}

  reportShortage() {
    (document.activeElement as HTMLElement)?.blur();

    setTimeout(() => {
      const dialogRef = this.matDialog.open(AddUnitComponent, {
        width: '750px',
        panelClass: 'report-shortage-style',
        autoFocus: true,
        restoreFocus: false,
        disableClose: true,
      });
    }, 0);
  }
  goBack() {
    this.location.back();
  }
}
