// Matches UserResponseDto from AuthService
export interface User {
  userId: number;
  userName: string;
  emailAddress: string;
  role: string;
}

// Matches LoginDto from AuthService
export interface LoginRequest {
  email: string;
  password: string;
}

// Matches RegisterDto from AuthService
export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
  role: string;
}

// Matches AuthResponseDto from AuthService
export interface AuthResponse {
  token: string;
  role: string;
  message: string;
}
