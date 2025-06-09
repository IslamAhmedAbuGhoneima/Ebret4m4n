import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../auth/services/auth.service';
import { CityCenterService } from '../../../services/cityCenter.service';
import Swal from 'sweetalert2';

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
  adminOfgovernorate: any;
  userId: any;
  data: any;

  healthUnits: any;
  cityAdminName: any;
  constructor(
    private fb: FormBuilder,
    private route: Router,
    private location: Location,
    private _ActivatedRoute: ActivatedRoute,
    private _AuthService: AuthService,
    private _CityCenterService: CityCenterService
  ) {}

  ngOnInit() {
    this.createForm();

    this.adminOfgovernorate = this._AuthService.getUserGovernorate()!;
    this.cityAdminName = this._AuthService.getUserCity()!;
    this._AuthService
      .getHealthUnits(this.adminOfgovernorate, this.cityAdminName)
      .subscribe({
        next: (res) => {
          this.healthUnits = res.data;
        },
        error: (error) => {
          const containsNonArabic =
            /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(
              error.error.message
            );

          const finalMessage = containsNonArabic
            ? `يوجد مشكلة مؤقتة في النظام. نعتذر عن الإزعاج، 
     
       الرجاء إعادة المحاولة بعد قليل.`
            : error.error.message;

          Swal.fire({
            icon: 'error',
            title: 'عذراً، حدث خطأ',
            text: finalMessage,
            confirmButtonColor: '#127453',
            confirmButtonText: 'حسناً , إغلاق',
          });
          this.healthUnits = [];
        },
      });

    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.userId = params.get('userId');
      this.loadUserData();
    });
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
      healthCareCenterId: ['', [Validators.required]],
    });
  }
  saveNewData() {
    if (this.formEditProfileDoctor.valid && this.formEditProfileDoctor.dirty) {
      const MODEL = {
        firstName: this.firstName?.value,
        lastName: this.secondName?.value,
        email: this.email?.value,
        healthCareCenterId: this.healthCareCenterId?.value,
      };
      this._CityCenterService
        .editCityCenterOrganizerOrDoctor(this.userId, MODEL)
        .subscribe({
          next: (res) => {
            this.route.navigate(['/admins']);
          },
          error: (error) => {
            const containsNonArabic =
              /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(
                error.error.message
              );

            const finalMessage = containsNonArabic
              ? `يوجد مشكلة مؤقتة في النظام. نعتذر عن الإزعاج، 
     
       الرجاء إعادة المحاولة بعد قليل.`
              : error.error.message;

            Swal.fire({
              icon: 'error',
              title: 'عذراً، حدث خطأ',
              text: finalMessage,
              confirmButtonColor: '#127453',
              confirmButtonText: 'حسناً , إغلاق',
            });
          },
        });
    }
  }

  loadUserData() {
    this._CityCenterService.getOrganizerOrDoctorDetails(this.userId).subscribe({
      next: (res) => {
        this.data = res.data;
        const userData = {
          firstName: this.data.firstName,
          secondName: this.data.lastName,
          email: this.data.email,
          healthCareCenterId: this.data?.hcCenterId,
        };

        this.formEditProfileDoctor.patchValue(userData);
      },
      error: (error) => {
        const containsNonArabic =
          /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(
            error.error.message
          );

        const finalMessage = containsNonArabic
          ? `يوجد مشكلة مؤقتة في النظام. نعتذر عن الإزعاج، 
     
       الرجاء إعادة المحاولة بعد قليل.`
          : error.error.message;

        Swal.fire({
          icon: 'error',
          title: 'عذراً، حدث خطأ',
          text: finalMessage,
          confirmButtonColor: '#127453',
          confirmButtonText: 'حسناً , إغلاق',
        });
      },
    });
  }

  get firstName() {
    return this.formEditProfileDoctor.get('firstName');
  }
  get secondName() {
    return this.formEditProfileDoctor.get('secondName');
  }

  get city() {
    return this.formEditProfileDoctor.get('city');
  }
  get governorate() {
    return this.formEditProfileDoctor.get('governorate');
  }
  get healthCareCenterId() {
    return this.formEditProfileDoctor.get('healthCareCenterId');
  }
  get email() {
    return this.formEditProfileDoctor.get('email');
  }

  goBack() {
    this.location.back();
  }
}
