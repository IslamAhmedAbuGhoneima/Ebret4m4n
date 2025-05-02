import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-report-shortage',
  imports: [FormsModule, CommonModule],
  templateUrl: './report-shortage.component.html',
  styleUrl: './report-shortage.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class ReportShortageComponent implements OnInit {
  roleType: any;
  vaccines = [
    { name: 'اسم اللقاح', available: 20, requested: null },
    { name: 'اسم اللقاح', available: 50, requested: null },
    { name: 'اسم اللقاح', available: 50, requested: null },
    { name: 'اسم اللقاح', available: 40, requested: null },
    { name: 'اسم اللقاح', available: 30, requested: null },
  ];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog: MatDialogRef<ReportShortageComponent>
  ) {}
  ngOnInit(): void {
    this.roleType = localStorage.getItem('role-type');
  }
  close() {
    this.dialog.close();
  }
  submitOrder() {
    const payload = this.vaccines
      .filter((v) => v.requested && v.requested > 0)
      .map((v) => ({ name: v.name, requested: v.requested }));



      if(this.roleType=='city-admin'){
        //service city
      }else if(this.roleType=='city-center-admin'){
        //service city center admin
      }else{
        //service organizer
      }
    console.log(payload);
    //server
    // this.http.post('/api/orders', payload).subscribe({
    //   next: (res) => {
    //     alert('تم إرسال الطلب بنجاح');
    //   },
    //   error: (err) => {
    //     alert('حدث خطأ أثناء إرسال الطلب');
    //     console.error(err);
    //   },
    // });
    this.dialog.close();
  }
}
