import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { VaccinationEditComponent } from '../../../../../standalone/components/dialogs/vaccination-edit/vaccination-edit.component';

@Component({
  selector: 'app-parent-home-page',
  standalone: false,
  templateUrl: './parent-home-page.component.html',
  styleUrl: './parent-home-page.component.css',
})
export class ParentHomePageComponent implements OnInit {
  constructor(private matDialog: MatDialog) {}
  ngOnInit(): void {}

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
}
