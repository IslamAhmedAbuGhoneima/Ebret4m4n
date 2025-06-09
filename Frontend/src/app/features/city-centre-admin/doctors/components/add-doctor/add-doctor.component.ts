import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { passwordMatch } from '../../../../../core/customValidation/passwordMatch.validator';
import { AuthService } from '../../../../auth/services/auth.service';
import { CityCenterService } from '../../../services/cityCenter.service';
import { Router, RouterModule } from '@angular/router';
import Swal from 'sweetalert2';

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
  cityAdminName: any;
  adminOfgovernorate: any;

  healthUnits: any;
  constructor(
    private fb: FormBuilder,
    private location: Location,
    private _AuthService: AuthService,
    private _CityCenterService: CityCenterService,
    private route: Router
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

        healthCareCenterId: ['', [Validators.required]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: passwordMatch }
    );
  }

  addDoctor() {
    if (this.addDoctorForm.valid) {
      const MODEL = {
        firstName: this.firstName?.value,
        lastName: this.secondName?.value,
        email: this.email?.value,
        password: this.password?.value,
        city: this.cityAdminName,
        governorate: this.adminOfgovernorate,
        healthCareCenterId: this.healthCareCenterId?.value,
        staffRole: 'doctor',
      };
      this._CityCenterService.addOrganizerOrDoctor(MODEL).subscribe({
        next: (res) => {
          this.route.navigate(['/city-center-admin/doctors']);
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

  get firstName() {
    return this.addDoctorForm.get('firstName');
  }
  get secondName() {
    return this.addDoctorForm.get('secondName');
  }

  get healthCareCenterId() {
    return this.addDoctorForm.get('healthCareCenterId');
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
    this.route.navigate(['/city-center-admin/doctors']);
  }
}
