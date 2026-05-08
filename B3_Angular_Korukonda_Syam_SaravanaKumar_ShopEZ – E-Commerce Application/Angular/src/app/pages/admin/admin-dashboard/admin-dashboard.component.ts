import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { forkJoin } from 'rxjs';
import { ProductService } from '../../../services/product.service';
import { OrderService } from '../../../services/order.service';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  // Loading state
  
  loading = true;

  // Dashboard counts

  productCount = 0;
  orderCount = 0;
  userCount = 0;

  // Quick navigation links

  links = [
    { route: '/admin/products', icon: 'bi-box-seam', label: 'Manage Products' },
    { route: '/admin/orders', icon: 'bi-receipt', label: 'Manage Orders' },
    { route: '/admin/users', icon: 'bi-people', label: 'View Users' },
    { route: '/products', icon: 'bi-grid', label: 'View Store' }
  ];

  constructor(
    private ps: ProductService,
    private os: OrderService,
    private as: AuthService
  ) {}

  ngOnInit(): void {

    // Fetch all dashboard data in parallel

    forkJoin([
      this.ps.getAll(),
      this.os.getAllOrders(),
      this.as.getAllUsers()
    ]).subscribe({
      next: ([products, orders, users]) => {

        // Safe fallback using optional chaining

        this.productCount = products?.length ?? 0;
        this.orderCount   = orders?.length ?? 0;
        this.userCount    = users?.length ?? 0;

        this.loading = false;
      },

      error: () => {
        // Stop loader on error

        this.loading = false;
      }
    });
  }
}