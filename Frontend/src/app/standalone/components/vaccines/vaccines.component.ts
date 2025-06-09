import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ReportShortageComponent } from '../dialogs/report-shortage/report-shortage.component';
import { AuthService } from '../../../features/auth/services/auth.service';
import { InventoryService } from '../../../core/services/inventory.service';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';

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
        error: (error) => {
          const containsNonArabic =
            /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(
              error.error.message
            );

          const finalMessage = containsNonArabic
            ? `يوجد مشكلة مؤقتة في النظام. نعتذر عن الإزعاج، 
     
       الرجاء إعادة المحاولة بعد قليل.`
            : error.error.message;

          Swal.fire({
            icon: 'error',
            title: 'عذراً، حدث خطأ',
            text: finalMessage,
            confirmButtonColor: '#127453',
            confirmButtonText: 'حسناً , إغلاق',
          });
        },
      });
    } else if (this.role == 'organizer') {
      this.inventoryService.getOrganizerInventory().subscribe({
        next: (res) => {
          this.data = res.data?.length ? res.data : null;
        },
        error: (error) => {
          const containsNonArabic =
            /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(
              error.error.message
            );

          const finalMessage = containsNonArabic
            ? `يوجد مشكلة مؤقتة في النظام. نعتذر عن الإزعاج، 
     
       الرجاء إعادة المحاولة بعد قليل.`
            : error.error.message;

          Swal.fire({
            icon: 'error',
            title: 'عذراً، حدث خطأ',
            text: finalMessage,
            confirmButtonColor: '#127453',
            confirmButtonText: 'حسناً , إغلاق',
          });
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
      this.inventoryService.adminCreateInventory({}).subscribe({
        next: (res) => {
          this.getServiceInventory();
        },
        error: (error) => {
          const containsNonArabic =
            /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(
              error.error.message
            );

          const finalMessage = containsNonArabic
            ? `يوجد مشكلة مؤقتة في النظام. نعتذر عن الإزعاج، 
     
       الرجاء إعادة المحاولة بعد قليل.`
            : error.error.message;

          Swal.fire({
            icon: 'error',
            title: 'عذراً، حدث خطأ',
            text: finalMessage,
            confirmButtonColor: '#127453',
            confirmButtonText: 'حسناً , إغلاق',
          });
        },
      });
    } else if (this.role == 'organizer') {
      this.inventoryService.organizerCreateInventory({}).subscribe({
        next: (res) => {
          this.data = res.data?.length ? res.data : null;
        },
        error: (error) => {
          const containsNonArabic =
            /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(
              error.error.message
            );

          const finalMessage = containsNonArabic
            ? `يوجد مشكلة مؤقتة في النظام. نعتذر عن الإزعاج، 
     
       الرجاء إعادة المحاولة بعد قليل.`
            : error.error.message;

          Swal.fire({
            icon: 'error',
            title: 'عذراً، حدث خطأ',
            text: finalMessage,
            confirmButtonColor: '#127453',
            confirmButtonText: 'حسناً , إغلاق',
          });
        },
      });
    }
  }
}
