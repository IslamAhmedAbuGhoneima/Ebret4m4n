import { Component, OnInit } from '@angular/core';
import { HealthMinistryService } from '../../../services/health-ministry.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-governorate',
  standalone: false,
  templateUrl: './governorate.component.html',
  styleUrl: './governorate.component.css',
})
export class GovernorateComponent implements OnInit {
  data: any;
  governorateName: string | undefined;

  constructor(
    private _ActivatedRoute: ActivatedRoute,
    private _HealthService: HealthMinistryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.governorateName = params.get('governorateName')!;
      if (this.governorateName) this.GovernorateDetails();
    });
  }
  GovernorateDetails() {
    this._HealthService.getGovernorateDetails(this.governorateName!).subscribe({
      next: (res) => {
        this.data = res.data;
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

  goBack() {
    this.router.navigate(['/health-ministry/governorates']);
  }
}
