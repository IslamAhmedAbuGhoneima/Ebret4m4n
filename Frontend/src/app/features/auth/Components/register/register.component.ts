import { Component, OnInit, ViewEncapsulation, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { passwordMatch } from '../../../../core/customValidation/passwordMatch.validator';
import { AuthService } from '../../services/auth.service';
import { Register } from '../../../../core/interfaces/register';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class RegisterComponent implements OnInit {
  firstFormGroup!: FormGroup;
  secondFormGroup!: FormGroup;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;
  errorMessage: any;

  constructor(
    private fb: FormBuilder,
    private route: Router,
    private _AuthService: AuthService
  ) {}

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.firstFormGroup = this.fb.group(
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
        phone: ['', [Validators.required, Validators.minLength(11)]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: passwordMatch }
    );
    this.secondFormGroup = this.fb.group({
      city: ['', [Validators.required]],
      town: ['', [Validators.required]],
      healthUnit: ['', [Validators.required]],
    });
  }

  register() {
    if (this.secondFormGroup.valid) {
      const model: Register = this.formatData();
      console.log(JSON.stringify(model));
      this._AuthService.signUp(model).subscribe({
        next: (res) => {
          this.route.navigate(['/parent']);
        },
        error: (error) => {
          this.errorMessage = error.error.message;
        },
      });
    } else {
      this.secondFormGroup.setErrors({ mismatch: true });
      this.secondFormGroup.markAllAsTouched();
    }
  }

  get firstName() {
    return this.firstFormGroup.get('firstName');
  }
  get secondName() {
    return this.firstFormGroup.get('secondName');
  }

  get phone() {
    return this.firstFormGroup.get('phone');
  }
  get email() {
    return this.firstFormGroup.get('email');
  }

  get password() {
    return this.firstFormGroup.get('password');
  }

  get confirmPassword() {
    return this.firstFormGroup.get('confirmPassword');
  }

  get city() {
    return this.secondFormGroup.get('city');
  }
  get town() {
    return this.secondFormGroup.get('town');
  }
  get healthUnit() {
    return this.secondFormGroup.get('healthUnit');
  }
  togglePassword() {
    this.showPassword = !this.showPassword;
  }
  toggleConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  formatData(): Register {
    return {
      firstName: this.firstFormGroup.value.firstName,
      lastName: this.firstFormGroup.value.secondName,
      email: this.firstFormGroup.value.email,
      password: this.firstFormGroup.value.password,
      phoneNumber: this.firstFormGroup.value.phone,

      governorate: this.secondFormGroup.value.city,
      city: this.secondFormGroup.value.town,
      village: this.secondFormGroup.value.healthUnit,
      role: 'parent',
      healthCareCenterId: 'F51D0B28-172B-4591-97E0-9E0D203CD3CA',
    };
  }
}
