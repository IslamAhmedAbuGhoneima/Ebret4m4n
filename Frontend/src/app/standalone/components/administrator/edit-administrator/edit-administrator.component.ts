import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule, Location } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
@Component({
  selector: 'app-edit-admin',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './edit-administrator.component.html',
  styleUrl: './edit-administrator.component.css',
})
export class EditAdministratorComponent implements OnInit {
  formEditProfileAdmin!: FormGroup;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;

  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location
  ) {}

  ngOnInit() {
    this.createForm();
    this.loadUserData();
  }

  createForm() {
    this.formEditProfileAdmin = this.fb.group({
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
    });
  }

  addAdmin() {
    if (this.formEditProfileAdmin.valid) {
      console.log('valid');
      // let model: VMHttp = {
      //   username: this.formEditProfileAdmin.value['username'],
      //   email: this.formEditProfileAdmin.value['email'],
      //   password: this.formEditProfileAdmin.value['password'],
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
      center: 'المنيا',
    };

    this.formEditProfileAdmin.patchValue(userData);
  }

  saveNewData() {
    if (this.formEditProfileAdmin.valid && this.formEditProfileAdmin.dirty) {
      console.log('valid');
    } else {
    }
  }

  get firstName() {
    return this.formEditProfileAdmin.get('firstName');
  }
  get secondName() {
    return this.formEditProfileAdmin.get('secondName');
  }

  get center() {
    return this.formEditProfileAdmin.get('center');
  }
  get email() {
    return this.formEditProfileAdmin.get('email');
  }

  goBack() {
    this.location.back();
  }
}
