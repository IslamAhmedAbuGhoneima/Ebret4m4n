import { Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-forget-password',
  standalone: false,
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.css',
})
export class ForgetPasswordComponent {
  forgetPasswordForm!: FormGroup;
  constructor(private fb: FormBuilder, private router: Router) {}
  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.forgetPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      // role: ['user'],
    });
  }
  verifyEmail() {
    if (this.forgetPasswordForm.valid) {
      console.log('valid');
      // this._apiService.signIn(this.forgetPasswordForm.value).subscribe({
      //   next: (response: any) => {
      //     localStorage.setItem('user_token', response.token);
      //     this.router.navigate(['/tasks']);
      //   },
      // });

      this.router.navigate(['/auth/change-password']);
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
