import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from '../../../../core/interfaces/order';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { HealthMinistryService } from '../../../../features/health-ministry-admin/services/health-ministry.service';
import { GovernorateAdminService } from '../../../../features/city-admin/services/governorateAdmin.service';
import { CityCenterService } from '../../../../features/city-centre-admin/services/cityCenter.service';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-orders',
  imports: [CommonModule, FormsModule],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css',
})
export class OrdersComponent implements OnInit {
  allOrders: any[] = [];
  filteredOrders: any[] = [];
  searchTerm: any;
  activeFilter: string = 'all';
  selectedOrder: any | null = null;
  role: any;
  pendingOrderCount: any;
  selectedOrderId: any;
  hasSentDoses: boolean = false;
  governorateName: any;
  hasSentDosesMap = new Map<number, boolean>(); // جديد

  constructor(
    private router: Router,
    private _AuthService: AuthService,
    private _HealthMinistryService: HealthMinistryService,
    private _GovernorateAdminService: GovernorateAdminService,
    private _CityCenterService: CityCenterService
  ) {}
  ngOnInit() {
    this.role = this._AuthService.getRole();
    this.governorateName = this._AuthService.getUserGovernorate();
    this.loadOrders();
  }

  loadOrders(resetFilter: boolean = true) {
    if (this.role == 'admin') {
      this._HealthMinistryService.getOrders().subscribe({
        next: (res) => {
          this.allOrders = this.formateData(res.data);
          this.filteredOrders = [...this.allOrders];

          this.pendingOrderCount = this.allOrders.filter(
            (item: any) => item.status === 'Pending'
          ).length;
          if (!resetFilter) {
            this.applyFilters();
          }
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
    } else if (this.role == 'governorateAdmin') {
      this._GovernorateAdminService.getOrders().subscribe({
        next: (res) => {
          this.allOrders = this.formateData(res.data);
          this.filteredOrders = [...this.allOrders];

          this.pendingOrderCount = this.allOrders.filter(
            (item: any) => item.status === 'Pending'
          ).length;
          if (!resetFilter) {
            this.applyFilters();
          }
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
    } else if (this.role == 'cityAdmin') {
      this._CityCenterService.getOrders().subscribe({
        next: (res) => {
          this.allOrders = this.formateData(res.data);
          this.filteredOrders = [...this.allOrders];
          this.pendingOrderCount = this.allOrders.filter(
            (item: any) => item.status === 'Pending'
          ).length;
          if (!resetFilter) {
            this.applyFilters();
          }
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

  filterOrders(status: string) {
    this.activeFilter = status;
    this.selectedOrder = null;
    this.applyFilters();
  }

  sendDoses() {
    if (!this.selectedOrderId) return;
    if (this.role == 'admin') {
      this._HealthMinistryService
        .acceptGovernorateOrder(this.selectedOrderId)
        .subscribe({
          next: (res) => {
            const index = this.allOrders.findIndex(
              (order) => order.id === this.selectedOrderId
            );

            if (index !== -1) {
              this.allOrders[index].status = 'Recived';
              this.allOrders[index].statusAr = 'مستلمة';
            }

            this.selectedOrderId = null;
            this.selectedOrder = null;
            this.hasSentDosesMap.set(this.selectedOrderId, true);

            this.applyFilters();

            this.pendingOrderCount = this.allOrders.filter(
              (item: any) => item.status === 'Pending'
            ).length;

            Swal.fire({
              title: res.data,
              text: 'جاري توصيل الطلب',
              icon: 'success',
              showCancelButton: true,
              showConfirmButton: false,
              confirmButtonColor: '#127453',
              cancelButtonColor: '#127453',
              cancelButtonText: 'حسناً , إغلاق',
              allowOutsideClick: false,
            });
          },
          error: (err) => {},
        });
    } else if (this.role == 'governorateAdmin' || this.role == 'cityAdmin') {
      this._GovernorateAdminService
        .acceptOrders(this.selectedOrderId)
        .subscribe({
          next: (res) => {
            const index = this.allOrders.findIndex(
              (order) => order.id === this.selectedOrderId
            );

            if (index !== -1) {
              this.allOrders[index].status = 'Recived';
              this.allOrders[index].statusAr = 'مستلمة';
            }

            this.selectedOrderId = null;
            this.selectedOrder = null;
            this.hasSentDosesMap.set(this.selectedOrderId, true);

            this.applyFilters();

            this.pendingOrderCount = this.allOrders.filter(
              (item: any) => item.status === 'Pending'
            ).length;
            Swal.fire({
              title: res.data,
              text: 'جاري توصيل الطلب',
              icon: 'success',
              showCancelButton: true,
              showConfirmButton: false,
              confirmButtonColor: '#127453',
              cancelButtonColor: '#127453',
              cancelButtonText: 'حسناً , إغلاق',
              allowOutsideClick: false,
            });
          },
          error: (error) => {const containsNonArabic =
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
          });},
        });
    }
  }

  formateData(dataArray: any[]): any[] {
    if (!Array.isArray(dataArray)) {
      return [];
    }

    return dataArray.map((item) => {
      let st = item.status;

      if (item.status == 'Pending') {
        st = 'قيد الإنتظار';
      } else if (item.status == 'Processing') {
        st = 'جارى التوصيل';
      } else if (item.status == 'Recived') {
        st = 'مستلمة';
      }

      return {
        ...item,
        statusAr: st,
      };
    });
  }
  selectOrder(orderId: any, orderStatus: any) {
    this.selectedOrderId = orderId;

    this._HealthMinistryService.getOrderDetails(orderId).subscribe({
      next: (res) => {
        this.selectedOrder = res.data.map((item: any) => ({
          ...item,
          orderStatus: orderStatus,
        }));
      },
      error: (err) => {},
    });
  }
  applyFilters() {
    this.filteredOrders = this.allOrders
      .filter(
        (o) => this.activeFilter === 'all' || o.status === this.activeFilter
      )
      .filter((o) => this.matchesSearch(o));
  }
  getDotIcon(status: string): string {
    switch (status) {
      case 'Pending':
        return '/icons/Dot.svg';
      case 'Recived':
        return '/icons/Dot2.svg';
      case 'Processing':
        return '/icons/Dot4.svg';
      default:
        return '';
    }
  }

  navigateTo(event: any) {
    if (event.target.value === 'all-orders') {
      this.router.navigate(['/orders']);
    } else if (event.target.value === 'my-orders') {
      this.router.navigate(['/orders/my-orders']);
    }
  }
  get hasPendingOrder(): boolean {
    return this.selectedOrder?.some(
      (item: any) => item.orderStatus === 'Pending'
    );
  }
  get hasSentDosesForSelected(): boolean {
    return this.selectedOrderId
      ? this.hasSentDosesMap.get(this.selectedOrderId) ?? false
      : false;
  }
  get filteredOrderDetails() {
    if (!this.selectedOrder) return [];

    return this.selectedOrder.filter((item: any) =>
      item.antigen.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
  search(term: string) {
    this.searchTerm = term;
    this.applyFilters();
  }
  matchesSearch(order: any): boolean {
    const term = this.normalize(this.searchTerm);
    if (!term) return true;

    let target = '';

    switch (this.role) {
      case 'admin':
        target = this.normalize(order.governorate || '');
        break;
      case 'governorateAdmin':
        target = this.normalize(order.city || '');
        break;
      case 'cityAdmin':
        target = this.normalize(order.healthCareCenterName || '');
        break;
      default:
        return false;
    }

    return target.includes(term);
  }

  normalize(text: string): string {
    if (!text || typeof text !== 'string') return '';
    return text
      .toLowerCase()
      .replace(/[أإآ]/g, 'ا')
      .replace(/ال/g, '')
      .replace(/\s+/g, '') // إزالة المسافات
      .trim();
  }
}
