// Matches OrderItemResponseDto from OrderService
export interface OrderItemResponse {
  productId: number;
  productName: string;
  quantity: number;
  price: number;
  imageUrl?: string;
  subtotal: number;   // Computed: Price * Quantity
}

// Matches OrderResponseDto from OrderService
export interface OrderResponse {
  orderId: number;
  userId: number;
  orderDate: string;
  totalAmount: number;
  isCancelled: boolean;
  items: OrderItemResponse[];
}

// Matches OrderItemDto inside OrderDto — for direct (Buy Now) orders
export interface OrderItemDto {
  productId: number;
  quantity: number;
}

// Matches OrderDto from OrderService — direct order POST /api/orders
export interface PlaceOrderRequest {
  cartItems: OrderItemDto[];
}

// POST /api/orders/from-cart — OrderService fetches cart from CartService internally
// No request body needed — OrderService reads JWT userId and calls CartService
export interface PlaceOrderFromCartRequest {
  // Empty — OrderService reads userId from JWT and fetches cart internally
}
