import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { InventoryService } from '../../../../core/services/inventory.service';
import Swal from 'sweetalert2';

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
        Swal.fire({
          title: res.data,
          text: 'الرجاء الانتظار حتي يتم الموافقة علي طلبك',
          icon: 'success',
          showCancelButton: true,
          showConfirmButton: false,
          confirmButtonColor: '#127453',
          cancelButtonColor: '#127453',
          cancelButtonText: 'حسناً , إغلاق',
          allowOutsideClick: false,
        });
        this.close();
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
