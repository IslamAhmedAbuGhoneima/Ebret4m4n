import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AddVaccinesComponent } from '../../../../../standalone/components/dialogs/add-vaccines/add-vaccines.component';

@Component({
  selector: 'app-add-child',
  standalone: false,
  templateUrl: './add-child.component.html',
  styleUrl: './add-child.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class AddChildComponent implements OnInit {
  formAddChild!: FormGroup;
  showPassword: boolean = false;

  constructor(private fb: FormBuilder, private matDialog: MatDialog) {}
  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.formAddChild = this.fb.group({
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
      vaccines: [''],
    });
  }
  addChild() {
    if (this.formAddChild.valid) {
      console.log('valid');
      console.log(this.formAddChild.value);
      // this._apiService.signIn(this.formAddChild.value).subscribe({
      //   next: (response: any) => {
      //     localStorage.setItem('user_token', response.token);
      //     this.router.navigate(['/tasks']);
      //   },
      // });
    }
  }
  addVaccine() {
    (document.activeElement as HTMLElement)?.blur();

    setTimeout(() => {
      const dialogRef = this.matDialog.open(AddVaccinesComponent, {
        width: '690px',
        panelClass: 'dialog-add-vaccine-container',
        autoFocus: true,
        restoreFocus: false,
        disableClose: true,
      });

      dialogRef.afterClosed().subscribe((selectedVaccines: string[]) => {
        if (selectedVaccines && selectedVaccines.length > 0) {
          this.vaccines?.setValue(selectedVaccines);
        }
      });
    }, 0);
  }
  get childName() {
    return this.formAddChild.get('childName');
  }
  get id() {
    return this.formAddChild.get('NID');
  }
  get gender() {
    return this.formAddChild.get('gender');
  }
  get weight() {
    return this.formAddChild.get('weight');
  }
  get birthday() {
    return this.formAddChild.get('birthday') as FormGroup;
  }
  get day() {
    return this.formAddChild.get('birthday.day');
  }

  get month() {
    return this.formAddChild.get('birthday.month');
  }

  get year() {
    return this.formAddChild.get('birthday.year');
  }
  get medicalHistory() {
    return this.formAddChild.get('medicalHistory');
  }
  get medicalImages() {
    return this.formAddChild.get('medicalImages');
  }
  get vaccines() {
    return this.formAddChild.get('vaccines');
  }
}
