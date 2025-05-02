import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { passwordMatch } from '../../../../../core/customValidation/passwordMatch.validator';

@Component({
  selector: 'app-add-doctor',
  standalone: false,
  templateUrl: './add-doctor.component.html',
  styleUrl: './add-doctor.component.css',
})
export class AddDoctorComponent implements OnInit {
  addDoctorForm!: FormGroup;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;

  constructor(private fb: FormBuilder, private location: Location) {}

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.addDoctorForm = this.fb.group(
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
        medicalCenter: ['', [Validators.required]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: passwordMatch }
    );
  }

  addDoctor() {
    if (this.addDoctorForm.valid) {
      console.log('valid');
      console.log(this.addDoctorForm.value);
      // let model: VMHttp = {
      //   username: this.addDoctorForm.value['username'],
      //   email: this.addDoctorForm.value['email'],
      //   password: this.addDoctorForm.value['password'],
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
    return this.addDoctorForm.get('firstName');
  }
  get secondName() {
    return this.addDoctorForm.get('secondName');
  }

  get medicalCenter() {
    return this.addDoctorForm.get('medicalCenter');
  }
  get email() {
    return this.addDoctorForm.get('email');
  }

  get password() {
    return this.addDoctorForm.get('password');
  }

  get confirmPassword() {
    return this.addDoctorForm.get('confirmPassword');
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
