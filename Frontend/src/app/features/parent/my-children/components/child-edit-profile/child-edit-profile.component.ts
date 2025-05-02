import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-child-edit-profile',
  standalone: false,
  templateUrl: './child-edit-profile.component.html',
  styleUrl: './child-edit-profile.component.css',
})
export class ChildEditProfileComponent implements OnInit {
  formEditProfile!: FormGroup;
  msgError: string = '';
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
      childName: [
        '',
        [
          Validators.required,
          Validators.pattern(/^[\u0600-\u06FF\s]+$/),
          Validators.minLength(3),
        ],
      ],
      NID: ['', [Validators.required, Validators.pattern(/^.{14}$/)]],
      gender: ['', [Validators.required]],
      weight: [
        '',
        [Validators.required, Validators.pattern(/^(1|[1-9]\d*)(\.\d+)?$/)],
      ],
      birthday: this.fb.group({
        day: ['', Validators.required],
        month: ['', Validators.required],
        year: [
          '',
          [
            Validators.required,
            Validators.min(2025),
            Validators.pattern(/^\d{4}$/),
          ],
        ],
      }),
      medicalHistory: [''],
      medicalImages: [''],
    });
  }
  loadUserData() {
    const userData = {
      childName: 'يوسف',
      NID: '12345678912345',
      gender: 'ذكر',
      weight: '10',
      birthday: {
        day: '13',
        month: 'يناير',
        year: '2025',
      },
      medicalHistory: 'سكري',
      medicalImages: '',
    };

    this.formEditProfile.patchValue(userData);
  }

  saveNewData() {
    if (this.formEditProfile.valid && this.formEditProfile.dirty) {
      console.log('valid');
    } else {
      // this.msgError='لم '
    }
  }
  goBack() {
    this.location.back();
  }
  get childName() {
    return this.formEditProfile.get('childName');
  }
  get id() {
    return this.formEditProfile.get('NID');
  }
  get gender() {
    return this.formEditProfile.get('gender');
  }
  get weight() {
    return this.formEditProfile.get('weight');
  }
  get birthday() {
    return this.formEditProfile.get('birthday') as FormGroup;
  }
  get day() {
    return this.formEditProfile.get('birthday.day');
  }

  get month() {
    return this.formEditProfile.get('birthday.month');
  }

  get year() {
    return this.formEditProfile.get('birthday.year');
  }
  get medicalHistory() {
    return this.formEditProfile.get('medicalHistory');
  }
  get medicalImages() {
    return this.formEditProfile.get('medicalImages');
  }
}
