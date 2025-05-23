import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ReportShortageComponent } from '../dialogs/report-shortage/report-shortage.component';
import { AuthService } from '../../../features/auth/services/auth.service';
import { InventoryService } from '../../../core/services/inventory.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-vaccines',
  imports: [CommonModule],
  templateUrl: './vaccines.component.html',
  styleUrl: './vaccines.component.css',
})
export class VaccinesComponent implements OnInit {
  role: any;
  data: any;
  constructor(
    private matDialog: MatDialog,
    private _AuthService: AuthService,
    private inventoryService: InventoryService
  ) {}
  ngOnInit(): void {
    this.role = this._AuthService.getRole();
    this.getServiceInventory();
  }

  getServiceInventory() {
    if (this.role === 'governorateAdmin' || this.role === 'cityAdmin') {
      this.inventoryService.getAdminInventory().subscribe({
        next: (res) => {
          this.data = res.data?.length ? res.data : null;
        },
        error: (err) => {
          console.log('error : ', err.error);
        },
      });
    }
  }
  reportShortage() {
    let shortageItems = this.data.map((item: any) => ({
      ...item,
      requested: null,
    }));
    (document.activeElement as HTMLElement)?.blur();

    setTimeout(() => {
      const dialogRef = this.matDialog.open(ReportShortageComponent, {
        panelClass: 'report-shortage-style',
        autoFocus: true,
        restoreFocus: false,
        disableClose: true,
        data: shortageItems,
      });
    }, 0);
  }

  createInventory() {
    if (this.role === 'governorateAdmin' || this.role === 'cityAdmin') {
      const model = {};
      this.inventoryService.adminCreateInventory(model).subscribe({
        next: (res) => {
          this.getServiceInventory();
        },
        error: (err) => {
          console.log('error : ', err.error);
        },
      });
    }
  }
}
