import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';

@Component({
  selector: 'app-edit-doctor',
  standalone: false,
  templateUrl: './edit-doctor.component.html',
  styleUrl: './edit-doctor.component.css',
})
export class EditDoctorComponent implements OnInit {
  formEditProfileDoctor!: FormGroup;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;

  constructor(private fb: FormBuilder, private location: Location) {}

  ngOnInit() {
    this.createForm();
    this.loadUserData();
  }

  createForm() {
    this.formEditProfileDoctor = this.fb.group({
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
      healthUnit: ['', [Validators.required]],
    });
  }

  addAdmin() {
    if (this.formEditProfileDoctor.valid) {
      console.log('valid');
      // let model: VMHttp = {
      //   username: this.formEditProfileDoctor.value['username'],
      //   email: this.formEditProfileDoctor.value['email'],
      //   password: this.formEditProfileDoctor.value['password'],
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

  loadUserData() {
    const userData = {
      firstName: 'محمد',
      secondName: 'أحمد',
      email: 'someone@example.com',
      healthUnit: '',
    };

    this.formEditProfileDoctor.patchValue(userData);
  }

  saveNewData() {
    if (this.formEditProfileDoctor.valid && this.formEditProfileDoctor.dirty) {
      console.log('valid');
    } else {
    }
  }

  get firstName() {
    return this.formEditProfileDoctor.get('firstName');
  }
  get secondName() {
    return this.formEditProfileDoctor.get('secondName');
  }

  get healthUnit() {
    return this.formEditProfileDoctor.get('healthUnit');
  }
  get email() {
    return this.formEditProfileDoctor.get('email');
  }

  goBack() {
    this.location.back();
  }
}
