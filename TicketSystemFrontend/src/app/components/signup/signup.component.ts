import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CustomerRegisterDto, AdminUserRegisterDto } from '../../models/auth.models';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  signupType: 'customer' | 'admin' = 'customer';
  fullName: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  errorMessage: string = '';
  loading: boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onSignup(): void {
    if (!this.fullName || !this.email || !this.password || !this.confirmPassword) {
      this.errorMessage = 'Please fill in all fields';
      return;
    }

    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    if (this.password.length < 6) {
      this.errorMessage = 'Password must be at least 6 characters';
      return;
    }

    this.loading = true;
    this.errorMessage = '';

    if (this.signupType === 'customer') {
      const data: CustomerRegisterDto = {
        fullName: this.fullName,
        email: this.email,
        password: this.password
      };

      this.authService.customerRegister(data).subscribe({
        next: () => {
          this.router.navigate(['/tickets']);
        },
        error: (error) => {
          this.errorMessage = error.error || 'Registration failed. Please try again.';
          this.loading = false;
        }
      });
    } else {
      const data: AdminUserRegisterDto = {
        fullName: this.fullName,
        email: this.email,
        password: this.password
      };

      this.authService.adminRegister(data).subscribe({
        next: () => {
          this.router.navigate(['/admin/tickets']);
        },
        error: (error) => {
          this.errorMessage = error.error || 'Registration failed. Please try again.';
          this.loading = false;
        }
      });
    }
  }

  switchSignupType(type: 'customer' | 'admin'): void {
    this.signupType = type;
    this.errorMessage = '';
  }
}
