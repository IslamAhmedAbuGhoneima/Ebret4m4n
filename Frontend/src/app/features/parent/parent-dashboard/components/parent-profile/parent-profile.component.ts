import { Component, OnInit } from '@angular/core';
import { ParentService } from '../../../services/parent.service';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-parent-profile',
  standalone: false,
  templateUrl: './parent-profile.component.html',
  styleUrl: './parent-profile.component.css',
})
export class ParentProfileComponent implements OnInit {
  data: any;
  userId: any;

  healthcareDetails: any;
  constructor(
    private _ParentService: ParentService,
    private _ActivatedRoute: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this._ActivatedRoute.paramMap.subscribe((params) => {
      this.userId = params.get('id');
    });

    forkJoin({
      profile: this._ParentService.parentProfile(this.userId),
      healthcare: this._ParentService.ParentHealthcareDetails(),
    }).subscribe({
      next: (res) => {
        this.data = res.profile.data;
        this.healthcareDetails = res.healthcare.data;
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
}
