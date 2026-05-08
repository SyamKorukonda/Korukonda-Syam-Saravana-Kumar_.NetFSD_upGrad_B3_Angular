import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { OrderService } from '../../services/order.service';
import { OrderResponse } from '../../models/order.model';

@Component({
  selector: 'app-my-orders',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {

  orders$!: Observable<OrderResponse[]>;
  loading = true;
  cancellingId: number | null = null;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.orders$ = this.orderService.orders;
    this.orderService.getMyOrders().subscribe({
      next: () => this.loading = false,
      error: () => this.loading = false
    });
  }

  cancel(order: OrderResponse): void {
    if (!order || !order.orderId || order.isCancelled) return;
    if (!confirm(`Are you sure you want to cancel Order #${order.orderId}?`)) return;

    this.cancellingId = order.orderId;

    this.orderService.cancelOrder(order.orderId).subscribe({
      next: () => this.cancellingId = null,
      error: () => this.cancellingId = null
    });
  }

  // Fallback for broken images
  onImgErr(e: Event): void {
    (e.target as HTMLImageElement).src = 'assets/images/no-image.png';
  }
}