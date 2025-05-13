import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-go-to-mail',
  imports: [],
  templateUrl: './go-to-mail.component.html',
  styleUrl: './go-to-mail.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class GoToMailComponent {
  constructor(
    private dialog: MatDialogRef<GoToMailComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private router: Router
  ) {}
  close() {
    this.dialog.close();
  }
}
