// Matches Product model from ProductService
export interface Product {
  productId: number;
  name: string;
  description: string;
  price: number;
  stock: number;
  imageUrl: string | null;
  category: string;
}

// Matches ProductRequest DTO from ProductService (Admin create/update)
export interface ProductRequest {
  name: string;
  description: string;
  price: number;
  stock: number;
  imageUrl: string | null;
  category?: string;
}

// Matches ProductInfoDto used in CartService and OrderService HTTP clients
export interface ProductInfoDto {
  productId: number;
  name: string;
  price: number;
  stock: number;
  imageUrl: string | null;
}
