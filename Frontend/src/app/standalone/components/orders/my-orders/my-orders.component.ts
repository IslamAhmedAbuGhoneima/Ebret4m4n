import { Component, LOCALE_ID, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Order } from '../../../../core/interfaces/order';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { HealthMinistryService } from '../../../../features/health-ministry-admin/services/health-ministry.service';
import { GovernorateAdminService } from '../../../../features/city-admin/services/governorateAdmin.service';
import { registerLocaleData } from '@angular/common';
import localeAr from '@angular/common/locales/ar';
registerLocaleData(localeAr);

@Component({
  selector: 'app-my-orders',
  imports: [CommonModule],
  providers: [{ provide: LOCALE_ID, useValue: 'ar-EG' }],

  templateUrl: './my-orders.component.html',
  styleUrl: './my-orders.component.css',
})
export class MyOrdersComponent implements OnInit {
  allOrders: any[] = [];
  filteredOrders: any[] = [];
  searchTerm: string = '';
  activeFilter: string = 'all';
  selectedOrder: any | null = null;
  role: any;
  processingOrderCount: any;
  selectedOrderId: any;
  acceptedDoses: boolean = false;
  governorateName: any;
  receivedMap = new Map<number, boolean>();

  constructor(
    private router: Router,
    private _AuthService: AuthService,
    private _GovernorateAdminService: GovernorateAdminService
  ) {}
  ngOnInit() {
    this.role = this._AuthService.getRole();
    this.governorateName = this._AuthService.getUserGovernorate();
    this.loadOrders();
  }

  loadOrders(resetFilter: boolean = true) {
    if (this.role === 'governorateAdmin' || this.role == 'cityAdmin') {
      this._GovernorateAdminService.getMyOrders().subscribe({
        next: (res) => {
          this.allOrders = this.formateData(res.data);
          this.filteredOrders = [...this.allOrders];

          this.processingOrderCount = this.allOrders.filter(
            (item: any) => item.status === 'Processing'
          ).length;
          if (!resetFilter) {
            this.applyFilters();
          }
        },
        error: (err) => {},
      });
    }
  }
  acceptDoses() {
    if (!this.selectedOrderId) return;
    if (this.role == 'governorateAdmin' || this.role == 'cityAdmin') {
      this._GovernorateAdminService
        .markReceivedOrder(this.selectedOrderId)
        .subscribe({
          next: () => {
            this.receivedMap.set(this.selectedOrderId!, true);
            this.loadOrders(false);
          },
          error: (err) => {},
        });
    }
  }
  filterOrders(status: string) {
    this.activeFilter = status;
    this.selectedOrder = null;
    this.applyFilters();
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
    if (this.role == 'governorateAdmin' || this.role == 'cityAdmin') {
      this._GovernorateAdminService.getOrderDetails(orderId).subscribe({
        next: (res) => {
          this.selectedOrder = res.data.map((item: any) => ({
            ...item,
            orderStatus: orderStatus,
          }));
        },
        error: (err) => {},
      });
    }
  }
  applyFilters() {
    this.filteredOrders = this.allOrders
      .filter(
        (o) => this.activeFilter === 'all' || o.status === this.activeFilter
      )
      .filter((o) => !this.searchTerm || o.center?.includes(this.searchTerm));
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

  get hasProcessingOrder(): boolean {
    return this.selectedOrder?.some(
      (item: any) => item.orderStatus === 'Processing'
    );
  }
  get hasReceivedDosesForSelected(): boolean {
    return this.selectedOrderId
      ? this.receivedMap.get(this.selectedOrderId) ?? false
      : false;
  }
}
