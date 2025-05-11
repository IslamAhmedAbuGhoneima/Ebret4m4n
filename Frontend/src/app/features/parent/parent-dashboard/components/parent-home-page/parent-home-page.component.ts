import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { VaccinationEditComponent } from '../../../../../standalone/components/dialogs/vaccination-edit/vaccination-edit.component';
import { ParentService } from '../../../services/parent.service';

@Component({
  selector: 'app-parent-home-page',
  standalone: false,
  templateUrl: './parent-home-page.component.html',
  styleUrl: './parent-home-page.component.css',
})
export class ParentHomePageComponent implements OnInit {
  data: any;
  errorMessage: string ='';
  constructor(
    private matDialog: MatDialog,
    private _ParentService: ParentService
  ) {}
  ngOnInit(): void {
    this.getReservations();
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
  getReservations() {
    this._ParentService.reservations().subscribe({
      next: (res) => {
        this.data = res;
      },
      error: (err) => {
        this.errorMessage = err.error.message;
      },
    });
  }
}
