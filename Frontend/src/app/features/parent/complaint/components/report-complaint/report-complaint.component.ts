import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ParentService } from '../../../services/parent.service';

@Component({
  selector: 'app-report-complaint',
  standalone: false,
  templateUrl: './report-complaint.component.html',
  styleUrl: './report-complaint.component.css',
})
export class ReportComplaintComponent implements OnInit {
  formComplaint!: FormGroup;
  errorMessage: any;
  constructor(private fb: FormBuilder, private _ParentService: ParentService) {}
  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.formComplaint = this.fb.group({
      heathCareUnit: [''],
      complaint: ['', [Validators.required]],
    });
  }
  submit() {
    if (this.formComplaint.valid) {
      const model = { message: this.formComplaint.value.complaint };
      this._ParentService.addComplaint(model).subscribe({
        next: (res) => {
          this.errorMessage = res.data;
        },
        error: (error) => {
          this.errorMessage = error.error.Message;
        },
      });
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
