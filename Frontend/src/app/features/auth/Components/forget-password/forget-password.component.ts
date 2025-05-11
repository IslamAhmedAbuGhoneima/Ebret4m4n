import { Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

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
    private authService: AuthService
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
        next: (res) => {
          console.log(res);
        },
        error: (err) => {
          this.errorMessage = err.error.Message || 'حدث خطأ أثناء إرسال الطلب';
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
