import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-contact-us',
  imports: [RouterModule, CommonModule, ReactiveFormsModule],
  templateUrl: './contact-us.component.html',
  styleUrl: './contact-us.component.css',
})
export class ContactUsComponent implements OnInit {
  formComplaint!: FormGroup;
  constructor(private fb: FormBuilder, private router: Router) {}

  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.formComplaint = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      complaint: ['', [Validators.required]],
    });
  }
  submit() {
    if (this.formComplaint.valid) {
      const model = { message: this.formComplaint.value.complaint };
      // this._ParentService.addComplaint(model).subscribe({
      //   next: (res) => {
      //     this.errorMessage = res.data;
      //   },
      //   error: (error) => {
      //     this.errorMessage = error.error.Message;
      //   },
      // });
    } else {
      this.markAllAsDirty(this.formComplaint);
    }
  }
  get email() {
    return this.formComplaint.get('email');
  }

  get complaint() {
    return this.formComplaint.get('complaint');
  }
  markAllAsDirty(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach((control) => {
      control.markAsDirty();
      if ((control as FormGroup).controls) {
        this.markAllAsDirty(control as FormGroup); // في حالة وجود FormGroup داخلية
      }
    });
  }

  goBack() {
    this.router.navigate(['/home']);
  }
}
