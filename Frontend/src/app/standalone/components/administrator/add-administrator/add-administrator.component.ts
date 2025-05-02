import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule, Location } from '@angular/common';
import { passwordMatch } from '../../../../core/customValidation/passwordMatch.validator';

@Component({
  selector: 'app-add-administrator',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-administrator.component.html',
  styleUrl: './add-administrator.component.css',
})
export class AddAdministratorComponent implements OnInit {
  addAdminForm!: FormGroup;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;

  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location
  ) {}

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.addAdminForm = this.fb.group(
      {
        firstName: [
          '',
          [
            Validators.required,
            Validators.pattern(/^[\u0600-\u06FF\s]+$/), // يسمح فقط بالحروف العربية والمسافات
            Validators.minLength(3),
            Validators.maxLength(20),
          ],
        ],
        secondName: [
          '',
          [
            Validators.required,
            Validators.pattern(/^[\u0600-\u06FF\s]+$/), // يسمح فقط بالحروف العربية والمسافات
            Validators.minLength(3),
            Validators.maxLength(20),
          ],
        ],
        email: ['', [Validators.required, Validators.email]],
        center: ['', [Validators.required]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: passwordMatch }
    );
  }

  addAdmin() {
    if (this.addAdminForm.valid) {
      console.log('valid');
      // let model: VMHttp = {
      //   username: this.addAdminForm.value['username'],
      //   email: this.addAdminForm.value['email'],
      //   password: this.addAdminForm.value['password'],
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
    }
  }

  get firstName() {
    return this.addAdminForm.get('firstName');
  }
  get secondName() {
    return this.addAdminForm.get('secondName');
  }

  get center() {
    return this.addAdminForm.get('center');
  }
  get email() {
    return this.addAdminForm.get('email');
  }

  get password() {
    return this.addAdminForm.get('password');
  }

  get confirmPassword() {
    return this.addAdminForm.get('confirmPassword');
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }
  toggleConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  goBack() {
    this.location.back();
  }
}
