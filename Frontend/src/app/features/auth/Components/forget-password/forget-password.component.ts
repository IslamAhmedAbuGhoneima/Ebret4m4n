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
        error: (error) => {
          const containsNonArabic =
            /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(
              error.error.message
            );

          const finalMessage = containsNonArabic
            ? `يوجد مشكلة مؤقتة في النظام. نعتذر عن الإزعاج، 
     
       الرجاء إعادة المحاولة بعد قليل.`
            : error.error.message;

          Swal.fire({
            icon: 'error',
            title: 'عذراً، حدث خطأ',
            text: finalMessage,
            confirmButtonColor: '#127453',
            confirmButtonText: 'حسناً , إغلاق',
          });
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
