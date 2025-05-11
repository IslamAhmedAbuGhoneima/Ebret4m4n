import { Component, Input, OnInit, model } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule, Location } from '@angular/common';
import { passwordMatch } from '../../../../core/customValidation/passwordMatch.validator';
import { AuthService } from '../../../../features/auth/services/auth.service';
import { AddAdmin } from '../../../../core/interfaces/AddAdmin';
import { GlobalService } from '../../../../core/services/APIs/global.service';

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
  role: string | null | undefined;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location,
    private _GlobalService: GlobalService,
    private _AuthService: AuthService
  ) {}

  ngOnInit() {
    this.role = this._AuthService.getRole();
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
        lastName: [
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
      const model = {
        firstName: this.addAdminForm.value.firstName,
        lastName: this.addAdminForm.value.lastName,
        email: this.addAdminForm.value.email,
        password: this.addAdminForm.value.password,
        governorate: this.addAdminForm.value.center,
      };

      this.getService(model);
      this.goBack();
    }
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }
  toggleConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  getService(model: AddAdmin) {
    if (this.role === 'admin') {
      this._GlobalService.addAdmin(model).subscribe({
        next: (res) => {},
        error: (err) => {
          this.errorMessage = err.error.Message;
        },
      });
    }
  }
  goBack() {
    this.location.back();
  }
  get firstName() {
    return this.addAdminForm.get('firstName');
  }
  get lastName() {
    return this.addAdminForm.get('lastName');
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
}
