import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from '../../../../core/interfaces/order';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { HealthMinistryService } from '../../../../features/health-ministry-admin/services/health-ministry.service';
import { GovernorateAdminService } from '../../../../features/city-admin/services/governorateAdmin.service';
import { CityCenterService } from '../../../../features/city-centre-admin/services/cityCenter.service';

@Component({
  selector: 'app-orders',
  imports: [CommonModule],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css',
})
export class OrdersComponent implements OnInit {
  allOrders: any[] = [];
  filteredOrders: any[] = [];
  searchTerm: string = '';
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
        error: (err) => {},
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
        error: (err) => {},
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
        error: (err) => {},
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
          next: () => {
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
          },
          error: (err) => {},
        });
    } else if (this.role == 'governorateAdmin' || this.role == 'cityAdmin') {
      this._GovernorateAdminService
        .acceptOrders(this.selectedOrderId)
        .subscribe({
          next: () => {
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
          },
          error: (err) => {},
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
}
