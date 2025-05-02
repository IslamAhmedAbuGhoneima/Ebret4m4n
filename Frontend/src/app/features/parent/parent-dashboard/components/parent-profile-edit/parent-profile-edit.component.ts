import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-parent-profile-edit',
  standalone: false,
  templateUrl: './parent-profile-edit.component.html',
  styleUrl: './parent-profile-edit.component.css',
})
export class ParentProfileEditComponent implements OnInit {
  formEditProfile!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location
  ) {}

  ngOnInit() {
    this.createForm();
    this.loadUserData();
    /*
    call API to fill Exist Email
     لو مستني حاجة من ايه بي اي يبقي لازم اعمل
     Async ValidatorFn

    */
  }

  createForm() {
    this.formEditProfile = this.fb.group({
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
      city: ['', [Validators.required]],
      town: ['', [Validators.required]],
      healthUnit: ['', [Validators.required]],
    });
  }
  loadUserData() {
    const userData = {
      firstName: 'محمد',
      secondName: 'أحمد',
      phone: '+20 123 456 7890',
      email: 'someone@example.com',
      city: 'المنيا',
      town: 'المنيا',
      healthUnit: 'المنيا',
    };

    this.formEditProfile.patchValue(userData);
  }

  saveNewData() {
    if (this.formEditProfile.valid && this.formEditProfile.dirty) {
      console.log('valid');
    } else {
    }
  }
  goBack() {
    this.location.back();
  }
  get firstName() {
    return this.formEditProfile.get('firstName');
  }
  get secondName() {
    return this.formEditProfile.get('secondName');
  }

  get phone() {
    return this.formEditProfile.get('phone');
  }
  get email() {
    return this.formEditProfile.get('email');
  }

  get city() {
    return this.formEditProfile.get('city');
  }
  get town() {
    return this.formEditProfile.get('town');
  }
  get healthUnit() {
    return this.formEditProfile.get('healthUnit');
  }
}
