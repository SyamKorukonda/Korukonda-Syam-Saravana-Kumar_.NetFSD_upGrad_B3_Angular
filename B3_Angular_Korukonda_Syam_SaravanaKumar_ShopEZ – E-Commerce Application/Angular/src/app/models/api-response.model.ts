// Matches ApiResponse<T> from ALL backend services exactly
export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}
