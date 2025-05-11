import { Component, Inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { ParentService } from '../../../../features/parent/services/parent.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-confirm',
  imports: [],
  templateUrl: './confirm.component.html',
  styleUrl: './confirm.component.css',
})
export class ConfirmComponent {
  constructor(
    private dialog: MatDialogRef<ConfirmComponent>,
    private _ParentService: ParentService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private router: Router
  ) {}

  confirm() {
    this._ParentService.deleteChild(this.data).subscribe({
      next: (res) => {
        this.dialog.close(true);
      },
      error: (err) => {
        this.dialog.close(false);
      },
    });
  }
  ignore() {
    this.dialog.close(false);
  }
}
