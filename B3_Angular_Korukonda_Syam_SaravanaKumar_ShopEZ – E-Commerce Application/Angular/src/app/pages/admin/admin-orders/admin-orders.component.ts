import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { OrderService } from '../../../services/order.service';
import { OrderResponse } from '../../../models/order.model';

@Component({
  selector: 'app-admin-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-orders.component.html',
  styleUrls: ['./admin-orders.component.css']
})
export class AdminOrdersComponent implements OnInit {

  // Stream of all orders

  orders$!: Observable<OrderResponse[]>;

  // UI states

  loading = true;
  cancellingId: number | null = null;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {

    // Bind observable from service

    this.orders$ = this.orderService.allOrders;

    // Fetch all orders from backend

    this.orderService.getAllOrders().subscribe({
      next: () => this.loading = false,
      error: () => this.loading = false
    });
  }

  // Cancel order with validation

  cancel(order: OrderResponse): void {

    // Prevent invalid cancel

    if (!order || !order.orderId || order.isCancelled) return;

    // Confirm action
    
    if (!confirm(`Cancel Order #${order.orderId}?`)) return;

    this.cancellingId = order.orderId;

    this.orderService.cancelOrder(order.orderId).subscribe({
      next: () => {
        this.cancellingId = null;
      },
      error: () => {
        this.cancellingId = null;
      }
    });
  }
}