import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { HealthMinistryService } from '../../../services/health-ministry.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-center',
  standalone: false,
  templateUrl: './center.component.html',
  styleUrl: './center.component.css',
})
export class CenterComponent implements OnInit {
  data: any;
  governorateName: any;
  centerName: any;
  centerId: any;
  constructor(
    private router: Router,
    private _HealthMinistryService: HealthMinistryService,
    private route: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.governorateName = params.get('governorateName')!;
      this.centerName = params.get('centerName')!;
      this.centerId = params.get('centerId')!;
      if (this.centerId) this.CenterDetails(this.centerId);
    });
  }
  CenterDetails(centerName: any) {
    this._HealthMinistryService.getCityCenterDetails(centerName).subscribe({
      next: (res) => {
        this.data = res.data;
      },
      error: (error) => {
       const containsNonArabic =
         /[a-zA-Z0-9!@#$%^&*(),.?":{}|<>[\]\\\/+=_-]/.test(error.error.message);

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
  goBack() {
    this.router.navigate([
      '/health-ministry/governorates',
      this.governorateName,
    ]);
  }
}
