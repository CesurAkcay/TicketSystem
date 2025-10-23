import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { 
  CustomerLoginDto, 
  CustomerRegisterDto, 
  AdminUserLoginDto, 
  AdminUserRegisterDto,
  AuthResponse,
  User
} from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/api/Auth'; // Update with your API URL
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadUserFromStorage();
  }

  // Customer Authentication
  customerLogin(credentials: CustomerLoginDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/loginCustomer`, credentials)
      .pipe(
        tap(response => this.handleAuthSuccess(response, 'Customer'))
      );
  }

  customerRegister(data: CustomerRegisterDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/registerCustomer`, data)
      .pipe(
        tap(response => this.handleAuthSuccess(response, 'Customer'))
      );
  }

  // Admin Authentication
  adminLogin(credentials: AdminUserLoginDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/loginAdminUser`, credentials)
      .pipe(
        tap(response => this.handleAuthSuccess(response, 'Admin'))
      );
  }

  adminRegister(data: AdminUserRegisterDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/registerAdminUser`, data)
      .pipe(
        tap(response => this.handleAuthSuccess(response, 'Admin'))
      );
  }

  // Handle successful authentication
  private handleAuthSuccess(response: AuthResponse, role: 'Customer' | 'Admin'): void {
    localStorage.setItem('token', response.token);
    localStorage.setItem('tokenExpiration', response.expiration);
    
    const user = this.decodeToken(response.token);
    if (user) {
      user.role = role;
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSubject.next(user);
    }
  }

  // Decode JWT token
  private decodeToken(token: string): User | null {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return {
        email: payload.email || payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
        fullName: payload.name || payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || '',
        role: payload.role || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
      };
    } catch (error) {
      return null;
    }
  }

  // Load user from storage
  private loadUserFromStorage(): void {
    const userStr = localStorage.getItem('user');
    if (userStr) {
      try {
        const user = JSON.parse(userStr);
        this.currentUserSubject.next(user);
      } catch (error) {
        this.logout();
      }
    }
  }

  // Logout
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('tokenExpiration');
    localStorage.removeItem('user');
    this.currentUserSubject.next(null);
  }

  // Get token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Check if user is authenticated
  isAuthenticated(): boolean {
    const token = this.getToken();
    const expiration = localStorage.getItem('tokenExpiration');
    
    if (!token || !expiration) {
      return false;
    }

    const expirationDate = new Date(expiration);
    return new Date() < expirationDate;
  }

  // Check if user is admin
  isAdmin(): boolean {
    const user = this.currentUserSubject.value;
    return user?.role === 'Admin';
  }

  // Get current user
  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }
}
