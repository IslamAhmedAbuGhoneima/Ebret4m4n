import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  formLogin!: FormGroup;
  showPassword: boolean = false;
  errorMessage: string = '';
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {}
  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.formLogin = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }
  login() {
    if (this.formLogin.valid) {
      this.authService.login(this.formLogin.value).subscribe({
        next: (res) => {
          this.navigateByRole();
        },
        error: (error) => {
          this.errorMessage = error.error.message;
        },
      });
    } else {
      this.markAllAsDirty(this.formLogin);
    }
  }
  get email() {
    return this.formLogin.get('email');
  }
  get password() {
    return this.formLogin.get('password');
  }
  togglePassword() {
    this.showPassword = !this.showPassword;
  }
  markAllAsDirty(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach((control) => {
      control.markAsDirty();
      if ((control as FormGroup).controls) {
        this.markAllAsDirty(control as FormGroup); // في حالة وجود FormGroup داخلية
      }
    });
  }
  navigateByRole(): void {
    const role = this.authService.getRole();
    switch (role) {
      case 'parent':
        this.router.navigate(['/parent']);
        break;
      case 'governorateAdmin': // city admin
        this.router.navigate(['/city-admin']);
        break;
      case 'cityAdmin': // city center admin
        this.router.navigate(['/city-center-admin']);
        break;
      case 'organizer':
        this.router.navigate(['/healthcare-organizer']);
        break;
      case 'admin':
        this.router.navigate(['/health-ministry']);
        break;
      case 'doctor':
        this.router.navigate(['/doctor']);
        break;
      default:
        this.router.navigate(['/auth/login']); // fallback
        break;
    }
  }
}
