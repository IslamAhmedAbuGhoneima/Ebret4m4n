import { Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-forget-password',
  standalone: false,
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.css',
})
export class ForgetPasswordComponent {
  forgetPasswordForm!: FormGroup;
  errorMessage: string = '';
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private matDialog: MatDialog
  ) {}
  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.forgetPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }
  verifyEmail() {
    if (this.forgetPasswordForm.valid) {
      this.authService.forgetPassword(this.forgetPasswordForm.value).subscribe({
        next: (res: any) => {
          Swal.fire({
            title: 'تنبيه',
            text: 'إذا كان بريدك الإلكتروني مسجلًا لدينا، فستصلك رسالة في صندوق الوارد قريبًا.',
            icon: 'info',
            showCancelButton: true,
            showConfirmButton: false,
            cancelButtonColor: '#127453',
            cancelButtonText: 'حسناً, سأذهب لفحص بريدي الإلكتروني ',
            allowOutsideClick: false,
          });
        },
        error: (err) => {
          this.errorMessage = err.error.Message;
        },
      });
    } else {
      this.markAllAsDirty(this.forgetPasswordForm);
    }
  }
  get email() {
    return this.forgetPasswordForm.get('email');
  }

  markAllAsDirty(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach((control) => {
      control.markAsDirty();
      if ((control as FormGroup).controls) {
        this.markAllAsDirty(control as FormGroup); // في حالة وجود FormGroup داخلية
      }
    });
  }
}
