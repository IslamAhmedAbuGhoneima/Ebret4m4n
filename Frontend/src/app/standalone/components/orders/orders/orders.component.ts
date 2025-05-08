import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from '../../../../core/interfaces/order';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-orders',
  imports: [CommonModule],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css',
})
export class OrdersComponent implements OnInit {
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
      { id: 1, status: 'قيد الإنتظار', center: 'المنيا' },
      { id: 2, status: 'مستلمة', center: 'المنيا' },
      { id: 3, status: 'غير مستلمة', center: 'المنيا' },
      { id: 4, status: 'جارى التوصيل', center: 'المنيا' },
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
    this.filteredOrders = this.allOrders
      .filter(
        (o) => this.activeFilter === 'الكل' || o.status === this.activeFilter
      )
      .filter((o) => !this.searchTerm || o.center?.includes(this.searchTerm));
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
