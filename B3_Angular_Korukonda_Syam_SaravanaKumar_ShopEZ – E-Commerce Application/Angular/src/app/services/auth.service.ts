import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap, map, catchError, throwError } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { User, LoginRequest, RegisterRequest, AuthResponse } from '../models/user.model';
import { environment } from '../../environments/environments';

@Injectable({ providedIn: 'root' })
export class AuthService {

  private readonly TOKEN_KEY = 'shopez_token';
  private readonly ROLE_KEY  = 'shopez_role';

  // BehaviorSubject — isLoggedIn$ — shared across Navbar, Guards, Cart

  private isLoggedIn$ = new BehaviorSubject<boolean>(this.hasToken());

  // BehaviorSubject — currentUser$ — holds role 'Admin' | 'Customer' | ''

  private currentUser$ = new BehaviorSubject<string>(this.getSavedRole());

  // Expose as read-only Observables

  isLoggedIn  = this.isLoggedIn$.asObservable();
  currentUser = this.currentUser$.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  // POST /gateway/auth/register — RegisterDto { name, email, password, role }

  register(dto: RegisterRequest): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(
      `${environment.gatewayUrl}/auth/register`, dto
    ).pipe(catchError(err => throwError(() => err)));
  }

  // POST /gateway/auth/login — LoginDto { email, password }
  // Returns AuthResponseDto { token, role, message }

  login(dto: LoginRequest): Observable<ApiResponse<AuthResponse>> {
    return this.http.post<ApiResponse<AuthResponse>>(
      `${environment.gatewayUrl}/auth/login`, dto
    ).pipe(
      tap(res => {
        if (res.success && res.data) {
          localStorage.setItem(this.TOKEN_KEY, res.data.token);
          localStorage.setItem(this.ROLE_KEY,  res.data.role);
          // Notify all subscribers — navbar, guards all react
          this.isLoggedIn$.next(true);
          this.currentUser$.next(res.data.role);
        }
      }),
      catchError(err => throwError(() => err))
    );
  }

  // Logout — clear storage and push false to all subscribers

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.ROLE_KEY);
    this.isLoggedIn$.next(false);
    this.currentUser$.next('');
    this.router.navigate(['/login']);
  }

  // GET /gateway/users — Admin only — returns UserResponseDto[]
  
  getAllUsers(): Observable<User[]> {
    return this.http.get<ApiResponse<User[]>>(
      `${environment.gatewayUrl}/users`
    ).pipe(
      map(res => res.data),
      catchError(err => throwError(() => err))
    );
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isAdmin(): boolean {
    return this.currentUser$.value === 'Admin';
  }

  private hasToken(): boolean {
    return !!localStorage.getItem(this.TOKEN_KEY);
  }

  private getSavedRole(): string {
    return localStorage.getItem(this.ROLE_KEY) ?? '';
  }
}
