import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-report-complaint',
  standalone: false,
  templateUrl: './report-complaint.component.html',
  styleUrl: './report-complaint.component.css',
})
export class ReportComplaintComponent implements OnInit {
  formComplaint!: FormGroup;
  constructor(private fb: FormBuilder) {}
  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.formComplaint = this.fb.group({
      heathCareUnit: ['', [Validators.required]],
      complaint: ['', [Validators.required]],
      // role: ['user'],
    });
  }
  submit() {
    if (this.formComplaint.valid) {
      console.log('valid');
      // this._apiService.signIn(this.formComplaint.value).subscribe({
      //   next: (response: any) => {
      //     localStorage.setItem('user_token', response.token);
      //     this.router.navigate(['/tasks']);
      //   },
      // });
    } else {
      this.markAllAsDirty(this.formComplaint);
    }
  }
  get heathCareUnit() {
    return this.formComplaint.get('heathCareUnit');
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
}
