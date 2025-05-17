import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { InventoryService } from '../../../../core/services/inventory.service';

@Component({
  selector: 'app-report-shortage',
  imports: [FormsModule, CommonModule],
  templateUrl: './report-shortage.component.html',
  styleUrl: './report-shortage.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class ReportShortageComponent implements OnInit {
  role: any;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog: MatDialogRef<ReportShortageComponent>,
    private inventoryService: InventoryService
  ) {}
  ngOnInit(): void {}
  close() {
    this.dialog.close();
  }
  submitOrder() {
    const payload = this.data
      .filter((v: any) => v.requested && v.requested > 0)
      .map((v: any) => ({
        antigen: v.antigen,
        amount: v.requested,
      }));

    this.inventoryService.vaccineOrder(payload).subscribe({
      next: (res) => {
        this.close();
      },
      error: (err) => {
        console.log('error : ', err.error);
      },
    });
  }
}
