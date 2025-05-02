import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { passwordMatch } from '../../../../core/customValidation/passwordMatch.validator';

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

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
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
      console.log('valid');
      // let model: VMHttp = {
      //   username: this.changePasswordForm.value['username'],
      //   email: this.changePasswordForm.value['email'],
      //   password: this.changePasswordForm.value['password'],
      //   role: 'user',
      // };
      // this._apiService.createAccount(model).subscribe({
      //   next: (response: any) => {
      //     // Use translation keys for Toastr messages
      //     this.toastr.success(
      //       this.translate.instant('REGISTER.SUCCESS_MESSAGE'),
      //       this.translate.instant('REGISTER.GREETING', {
      //         username: model.username,
      //       })
      //     );
      //     localStorage.setItem('user_token', response.token);
      //     this.router.navigate(['/tasks']);
      //   },
      // });
    } else {
      console.log('InValid');
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
