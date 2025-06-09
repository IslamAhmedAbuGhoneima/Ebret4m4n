import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { passwordMatch } from '../../../../core/customValidation/passwordMatch.validator';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-change-password',
  standalone: false,
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm!: FormGroup;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;

  userId: string = '';
  token: string = '';
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private _ActivatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this._ActivatedRoute.queryParams.subscribe((params) => {
      this.userId = params['userId'];
      this.token = params['token'];
    });
    this.createForm();
  }

  createForm() {
    this.changePasswordForm = this.fb.group(
      {
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: passwordMatch }
    );
  }

  changePassword() {
    if (this.changePasswordForm.valid) {
      const model = {
        userId: this.userId,
        token: this.token,
        newPassword: this.changePasswordForm.value.password,
      };

      this.authService.changePass(model).subscribe({
        next: (res) => {},
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
      this.changePasswordForm.setErrors({ mismatch: true });
      this.changePasswordForm.markAllAsTouched();
    }
  }

  get password() {
    return this.changePasswordForm.get('password');
  }

  get confirmPassword() {
    return this.changePasswordForm.get('confirmPassword');
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }
  toggleConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
}
