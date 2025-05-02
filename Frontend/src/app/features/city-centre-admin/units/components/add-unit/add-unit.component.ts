import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-add-unit',
  standalone: false,
  templateUrl: './add-unit.component.html',
  styleUrl: './add-unit.component.css',
})
export class AddUnitComponent implements OnInit {
  addUnitForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location
  ) {}

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.addUnitForm = this.fb.group({
      healthUnitName: ['', [Validators.required]],
      LocationOfHealthUnit: this.fb.group({
        city: ['', [Validators.required]],
        center: ['', [Validators.required]],
      }),

      WorkDays: this.fb.group({
        firstDay: ['', [Validators.required]],
        secondDay: ['', [Validators.required]],
      }),
    });
  }

  addUnit() {
    if (this.addUnitForm.valid) {
      console.log('valid');
      console.log(this.addUnitForm.value);
      // let model: VMHttp = {
      //   username: this.addUnitForm.value['username'],
      //   email: this.addUnitForm.value['email'],
      //   password: this.addUnitForm.value['password'],
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

  get healthUnitName() {
    return this.addUnitForm.get('healthUnitName');
  }

  get LocationOfHealthUnit() {
    return this.addUnitForm.get('LocationOfHealthUnit');
  }

  get city() {
    return this.addUnitForm.get('LocationOfHealthUnit.city');
  }
  get center() {
    return this.addUnitForm.get('LocationOfHealthUnit.center');
  }
  get workDays() {
    return this.addUnitForm.get('WorkDays');
  }
  get firstDay() {
    return this.addUnitForm.get('WorkDays.firstDay');
  }

  get secondDay() {
    return this.addUnitForm.get('WorkDays.secondDay');
  }

  goBack() {
    this.location.back();
  }
}
