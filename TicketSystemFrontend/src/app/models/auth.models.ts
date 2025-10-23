export interface CustomerLoginDto {
  email: string;
  password: string;
}

export interface CustomerRegisterDto {
  fullName: string;
  email: string;
  password: string;
}

export interface AdminUserLoginDto {
  email: string;
  password: string;
}

export interface AdminUserRegisterDto {
  email: string;
  password: string;
  fullName: string;
}

export interface AuthResponse {
  token: string;
  expiration: string;
}

export interface User {
  email: string;
  fullName: string;
  role: 'Customer' | 'Admin';
}
