import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from '../../../../core/interfaces/order';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { GlobalService } from '../../../../core/services/APIs/global.service';
import { HealthMinistryService } from '../../../../features/health-ministry-admin/services/health-ministry.service';

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
  activeFilter: string = 'all'; // مبدئيا الكل هو اللي مفعل
  selectedOrder: any | null = null;
  role: any;
  pendingOrderCount: any;
  selectedOrderId: any;
  hasSentDoses: boolean = false;

  constructor(
    private router: Router,
    private _AuthService: AuthService,
    private _HealthMinistryService: HealthMinistryService
  ) {}
  ngOnInit() {
    this.role = this._AuthService.getRole();
    this.loadOrders();
  }

  loadOrders() {
    if (this.role == 'admin') {
      this._HealthMinistryService.getOrders().subscribe({
        next: (res) => {
          this.allOrders = this.formateData(res.data);
          this.filteredOrders = [...this.allOrders];

          this.pendingOrderCount = this.allOrders.filter(
            (item: any) => item.status === 'Pending'
          ).length;
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

    this._HealthMinistryService
      .acceptGovernorateOrder(this.selectedOrderId)
      .subscribe({
        next: () => {
          this.hasSentDoses = true;

          this.loadOrders();
        },
        error: (err) => {},
      });
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
}
