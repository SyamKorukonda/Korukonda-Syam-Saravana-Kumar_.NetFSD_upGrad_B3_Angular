import { Routes } from '@angular/router';
import { authGuard, guestGuard } from './guards/auth.guard';
import { adminGuard } from './guards/admin.guard';

export const routes: Routes = [

  // Default redirect
  { path: '', redirectTo: 'home', pathMatch: 'full' },

  //  Public 
  {
    path: 'home',
    loadComponent: () =>
      import('./pages/home/home.component').then(m => m.HomeComponent)
  },

  //  Guest only — redirect to home if already logged in 
  {
    path: 'login',
    canActivate: [guestGuard],
    loadComponent: () =>
      import('./pages/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'register',
    canActivate: [guestGuard],
    loadComponent: () =>
      import('./pages/register/register.component').then(m => m.RegisterComponent)
  },

  //  Products — public 
  {
    path: 'products',
    loadComponent: () =>
      import('./product-list/product-list.component').then(m => m.ProductListComponent)
  },
  {
    path: 'products/:id',
    loadComponent: () =>
      import('./product-details/product-details.component').then(m => m.ProductDetailsComponent)
  },

  //Cart

  {
  path: 'cart',
  loadComponent: () =>
    import('./cart/cart.component').then(m => m.CartComponent)
  },

  //  Checkout — auth required 
  {
    path: 'checkout',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./checkout/checkout.component').then(m => m.CheckoutComponent)
  },

  //  My Orders — auth required 
  {
    path: 'my-orders',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./pages/my-orders/my-orders.component').then(m => m.MyOrdersComponent)
  },

  //  Admin — auth + admin guard 
  {
    path: 'admin',
    canActivate: [authGuard, adminGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        loadComponent: () =>
          import('./pages/admin/admin-dashboard/admin-dashboard.component').then(m => m.AdminDashboardComponent)
      },
      {
        path: 'products',
        loadComponent: () =>
          import('./pages/admin/admin-products/admin-products.component').then(m => m.AdminProductsComponent)
      },
      {
        path: 'orders',
        loadComponent: () =>
          import('./pages/admin/admin-orders/admin-orders.component').then(m => m.AdminOrdersComponent)
      },
      {
        path: 'users',
        loadComponent: () =>
          import('./pages/admin/admin-users/admin-users.component').then(m => m.AdminUsersComponent)
      }
    ]
  },

  //  Wildcard — redirect unknown routes to home 
  { path: '**', redirectTo: 'home' }
];