import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ReportShortageComponent } from '../dialogs/report-shortage/report-shortage.component';

@Component({
  selector: 'app-vaccines',
  imports: [],
  templateUrl: './vaccines.component.html',
  styleUrl: './vaccines.component.css',
})
export class VaccinesComponent implements OnInit {
  roleType: any;
  constructor(private matDialog: MatDialog) {}
  ngOnInit(): void {
    this.roleType = localStorage.getItem('role-type');
  }

  reportShortage() {
    (document.activeElement as HTMLElement)?.blur();

    setTimeout(() => {
      const dialogRef = this.matDialog.open(ReportShortageComponent, {
        width: '750px',
        panelClass: 'report-shortage-style',
        autoFocus: true,
        restoreFocus: false,
        disableClose: true,
      });
    }, 0);
  }
}
