import { Component, OnInit, ViewEncapsulation, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { passwordMatch } from '../../../../core/customValidation/passwordMatch.validator';

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

  constructor(private fb: FormBuilder, private route: Router) {}

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

  signUp() {
    if (this.secondFormGroup.valid) {
      console.log('valid');
      this.route.navigate(['/parent/home']); // this._apiService.signIn(this.formLogin.value).subscribe({

      // let model: VMHttp = {
      //   username: this.firstFormGroup.value['username'],
      //   email: this.firstFormGroup.value['email'],
      //   password: this.firstFormGroup.value['password'],
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

      this.route.navigate(['/parent/dashboard']);
    } else {
      console.log('InValid');
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
}
