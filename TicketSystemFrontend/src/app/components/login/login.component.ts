import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CustomerLoginDto, AdminUserLoginDto } from '../../models/auth.models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginType: 'customer' | 'admin' = 'customer';
  email: string = '';
  password: string = '';
  errorMessage: string = '';
  loading: boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onLogin(): void {
    if (!this.email || !this.password) {
      this.errorMessage = 'Please fill in all fields';
      return;
    }

    this.loading = true;
    this.errorMessage = '';

    if (this.loginType === 'customer') {
      const credentials: CustomerLoginDto = {
        email: this.email,
        password: this.password
      };

      this.authService.customerLogin(credentials).subscribe({
        next: () => {
          this.router.navigate(['/tickets']);
        },
        error: (error) => {
          this.errorMessage = error.error || 'Login failed. Please check your credentials.';
          this.loading = false;
        }
      });
    } else {
      const credentials: AdminUserLoginDto = {
        email: this.email,
        password: this.password
      };

      this.authService.adminLogin(credentials).subscribe({
        next: () => {
          this.router.navigate(['/admin/tickets']);
        },
        error: (error) => {
          this.errorMessage = error.error || 'Login failed. Please check your credentials.';
          this.loading = false;
        }
      });
    }
  }

  switchLoginType(type: 'customer' | 'admin'): void {
    this.loginType = type;
    this.errorMessage = '';
  }
}
