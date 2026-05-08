// Matches CartItemResponseDto from CartService exactly
// CartItem model has: CartItemId, UserId, ProductId, ProductName, Price, Quantity, Stock, ImageUrl, CreatedAt, Subtotal
export interface CartItemResponse {
  cartItemId: number;
  userId: number;
  productId: number;
  productName: string;
  price: number;          // Field is "Price" not "UnitPrice" in CartService
  quantity: number;
  stock: number;          // Stock included in response from CartService
  imageUrl: string | null;
  createdAt: string;
  subtotal: number;       // Computed: Price * Quantity
}

// Matches CartSummaryDto from CartService exactly
export interface CartSummary {
  items: CartItemResponse[];
  totalPrice: number;   // Total price of all items
  totalItems: number;   // Total count of items
}

// Matches AddToCartDto from CartService — only productId and quantity
export interface AddToCartRequest {
  productId: number;
  quantity: number;
}

// Matches UpdateCartDto from CartService — only quantity
export interface UpdateCartRequest {
  quantity: number;
}
