import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';
import { LayoutService } from '../../../Services/layout.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  email = '';
  password = '';
  errorMessage = '';

  private authService = inject(AuthService);
  private router = inject(Router);
  private layoutService = inject(LayoutService);

  login() {
    this.authService
      .login({ email: this.email, password: this.password, rememberMe: true })
      .subscribe({
        next: (res) => {
          // ensure there is data before accessing properties
          if (!res.hasError && res.data) {
            // 🟢 1) خزّني كل الداتا المهمة
            this.authService.saveToken(res.data.token);
            // save both userId (string) and employeeId (number) correctly
            this.authService.saveUserId(res.data.userId);
            this.authService.saveEmployeeId(res.data.employeeId);
            this.authService.saveRoles(res.data.roles ?? []);

            this.errorMessage = '';

            // 🟢 2) اقفلي الـ login overlay
            this.layoutService.closeLogin();

            // 🟢 3) روّحي على الـ dashboard
            this.router.navigate(['/pages/dashboard']);
          } else {
            this.errorMessage = res.errorMessage || 'Login failed';
          }
        },
        error: (err) => {
          console.error('FULL ERROR: ', err);
          if (err.status === 401) {
            this.errorMessage = err.error?.errorMessage || 'Invalid credentials';
            alert(this.errorMessage);
          } else {
            this.errorMessage = 'Error connecting to server';
            alert('Error connecting to server');
          }
        },
      });
  }

  close() {
    this.layoutService.closeLogin();
    this.router.navigate(['/pages/dashboard']);
  }

  openRegister() {
    this.layoutService.openRegister();
    this.router.navigate(['/pages/register']);
  }
}
