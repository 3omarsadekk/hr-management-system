import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../Services/auth.service';
import { Router } from '@angular/router';
import { ToastService } from '../../Services/toast.service';

@Component({
    selector: 'app-change-password',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
    fb = inject(FormBuilder);
    authService = inject(AuthService);
    router = inject(Router);
    toastService = inject(ToastService);

    showCurrentPassword = false;
    showNewPassword = false;
    showConfirmPassword = false;

    changePasswordForm: FormGroup = this.fb.group({
        currentPassword: ['', [Validators.required]],
        newPassword: ['', [Validators.required, Validators.minLength(6)]],
        confirmNewPassword: ['', [Validators.required]]
    }, { validators: this.passwordMatchValidator });

    passwordMatchValidator(g: FormGroup) {
        return g.get('newPassword')?.value === g.get('confirmNewPassword')?.value
            ? null : { mismatch: true };
    }

    onSubmit() {
        if (this.changePasswordForm.valid) {
            this.authService.changePassword(this.changePasswordForm.value).subscribe({
                next: (res) => {
                    this.toastService.success('Password changed successfully');
                    this.changePasswordForm.reset();
                },
                error: (err) => {
                    this.toastService.error('Failed to change password');
                    console.error(err);
                }
            });
        } else {
            this.changePasswordForm.markAllAsTouched();
        }
    }
}
