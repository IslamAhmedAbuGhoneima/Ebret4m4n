import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Order } from '../../../../core/models/order';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-my-orders',
  imports: [CommonModule],
  templateUrl: './my-orders.component.html',
  styleUrl: './my-orders.component.css',
})
export class MyOrdersComponent implements OnInit {
  allOrders: Order[] = [];
  filteredOrders: Order[] = [];
  searchTerm: string = '';
  activeFilter: string = 'الكل'; // مبدئيا الكل هو اللي مفعل
  selectedOrder: Order | null = null;

  constructor(private router: Router) {}
  ngOnInit() {
    this.loadOrders();
  }
  navigateTo(event: any) {
    if (event.target.value === 'all-orders') {
      this.router.navigate(['/orders']);
    } else if (event.target.value === 'my-orders') {
      this.router.navigate(['/orders/my-orders']);
    }
  }
  loadOrders() {
    // this.orderService.getOrders().subscribe((data) => {
    //   this.allOrders = data;
    //   this.applyFilters();
    // });
    this.allOrders = [
      { id: 1, status: 'قيد الإنتظار', order: 'أوردر#11' },
      { id: 2, status: 'مستلمة', order: 'أوردر#11' },
      { id: 3, status: 'غير مستلمة', order: 'أوردر#11' },
      { id: 4, status: 'جارى التوصيل', order: 'أوردر#11' },
    ];
    this.filteredOrders = [...this.allOrders]; // مبدئيًا نعرض الكل
  }

  filterOrders(status: string) {
    this.activeFilter = status;
    this.selectedOrder = null;
    this.applyFilters();
  }

  onSearch() {
    this.applyFilters();
  }

  applyFilters() {
    this.filteredOrders = this.allOrders.filter(
      (o) => this.activeFilter === 'الكل' || o.status === this.activeFilter
    );
    // .filter((o) => !this.searchTerm || o.center.includes(this.searchTerm));
  }

  selectOrder(order: Order) {
    this.selectedOrder = order;
  }

  sendDoses() {
    //   if (!this.selectedOrder) return;
    //   this.orderService.sendDoses(this.selectedOrder.id).subscribe(() => {
    //     this.loadOrders();
    //   });
  }

  getDotIcon(status: string): string {
    switch (status) {
      case 'قيد الإنتظار':
        return '/icons/Dot.svg';
      case 'مستلمة':
        return '/icons/Dot2.svg';
      case 'غير مستلمة':
        return '/icons/Dot3.svg';
      case 'جارى التوصيل':
        return '/icons/Dot4.svg';
      default:
        return '';
    }
  }
}
