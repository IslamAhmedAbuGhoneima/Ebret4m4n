import { Component, OnInit } from '@angular/core';
import { OrganizerService } from '../../services/organizer.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-vaccinated-children',
  standalone: false,
  templateUrl: './vaccinated-children.component.html',
  styleUrl: './vaccinated-children.component.css',
})
export class VaccinatedChildrenComponent implements OnInit {
  vaccinatedList: any[] = [];
  searchId: string = '';
  filteredList: any[] = [];
  constructor(private _OrganizerService: OrganizerService) {}
  ngOnInit(): void {
    const storedData = localStorage.getItem('vaccinatedList');

    if (storedData) {
      this.vaccinatedList = JSON.parse(storedData);
      this.filteredList = this.vaccinatedList;
    }
  }
  onSearch() {
    const id = this.searchId.trim();
    if (id) {
      this.filteredList = this.vaccinatedList.filter((child: any) =>
        child.childId?.toString().includes(id)
      );
    } else {
      this.filteredList = this.vaccinatedList;
    }
  }
  done() {
    this._OrganizerService.updateVaccineStatues(this.vaccinatedList).subscribe({
      next: (res) => {
        Swal.fire({
          title: res.data,
          text: 'تم تعديل حاله اللقاح لهؤلاء الاطفال',
          icon: 'success',
          showCancelButton: true,
          showConfirmButton: false,
          confirmButtonColor: '#127453',
          cancelButtonColor: '#127453',
          cancelButtonText: 'حسناً , إغلاق',
          allowOutsideClick: false,
        });

        localStorage.removeItem('vaccinatedList');
        this.vaccinatedList = [];
        this.filteredList = [];
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
}
