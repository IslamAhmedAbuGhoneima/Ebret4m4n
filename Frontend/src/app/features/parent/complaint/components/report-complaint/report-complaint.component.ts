import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ParentService } from '../../../services/parent.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-report-complaint',
  standalone: false,
  templateUrl: './report-complaint.component.html',
  styleUrl: './report-complaint.component.css',
})
export class ReportComplaintComponent implements OnInit {
  formComplaint!: FormGroup;
  errorMessage: any;
  data: any;
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
          Swal.fire({
            title: 'تم استلام شكواك بنجاح',
            text: 'شكرًا لتعاونك معنا، سيتم حل الشكوى الخاصة بك في أقرب وقت.',
            icon: 'success',
            showCancelButton: false,
            confirmButtonColor: '#127453',
            cancelButtonColor: '#B4231B',
            confirmButtonText: 'حسنا, إغلاق',
            allowOutsideClick: false,
          });

          this.formComplaint.reset();
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
