import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration } from 'chart.js';
import { ChartData, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { Chart } from 'chart.js';
import { GovernorateAdminService } from '../../services/governorateAdmin.service';
import Swal from 'sweetalert2';

Chart.register(ChartDataLabels);
@Component({
  selector: 'app-city-admin-dashboard',
  standalone: false,
  templateUrl: './city-admin-dashboard.component.html',
  styleUrl: './city-admin-dashboard.component.css',
})
export class CityAdminDashboardComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;
  @ViewChild(BaseChartDirective) chartBar:
    | BaseChartDirective<'bar'>
    | undefined;
  data: any = {};
  constructor(private _GovernorateAdminService: GovernorateAdminService) {}

  ngOnInit(): void {
    this.getStatistics();
  }
  getStatistics() {
    this._GovernorateAdminService.getStatisticsOfGovernorateAdmin().subscribe({
      next: (res) => {
        this.data = res;
        this.pieChartData = {
          labels: ['طفل', 'طفلة'],
          datasets: [
            {
              data: [this.data.maleChildren, this.data.femaleChildren],
              backgroundColor: ['#00ACF8', '#ec4899'],
            },
          ],
        };

        const labels = this.data.topCitiesByVaccines.map(
          (item: any) => item.city
        );
        const vaccines = this.data.topCitiesByVaccines.map(
          (item: any) => item.requestedAmount
        );

        this.barChartData.labels = labels;
        this.barChartData.datasets[0].data = vaccines;
        this.barChartData = { ...this.barChartData };
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
  // Pie
  public pieChartType: ChartType = 'pie';

  public pieChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
        position: 'bottom',
        labels: {
          padding: 20,
          boxWidth: 20,
          font: {
            family: 'Cairo',
            size: 14, // حجم الخط
            weight: 'bold', // نوع الخط
          },
          color: '#121212', // لون الخط
        },
      },
      datalabels: {
        color: '#fff',
        font: {
          family: 'Cairo',
          size: 10,
          weight: 'bold',
        },
        align: 'center', // تمركز عمودي
        anchor: 'center', // تمركز أفقي
        offset: 0,
        textAlign: 'center', // يضيف محاذاة داخل النص نفسه
        formatter: (value, ctx) => {
          const data = ctx.chart.data.datasets[0].data as number[];
          const total = data.reduce((sum, val) => sum + val, 0);
          const percentage = ((value / total) * 100).toFixed(1);
          return `${percentage}%`;
        },
      },
    },
  };
  public pieChartData: ChartData<'pie', number[], string | string[]> = {
    labels: ['طفل', 'طفلة'],
    datasets: [
      {
        data: [this.data.maleChildren, this.data.femaleChildren],
        backgroundColor: ['#00ACF8', '#ec4899'], // 💙 ذكر، 💗 أنثى
      },
    ],
  };

  // bar;
  public barChartType = 'bar' as const;

  public barChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: true,
    scales: {
      x: {
        ticks: {
          font: {
            family: 'Cairo',
            size: 14,
          },
        },
        grid: {
          display: false, // إخفاء خطوط الشبكة الرأسية
        },
      },
      y: {
        beginAtZero: true,
        ticks: {
          font: {
            family: 'Cairo',
            size: 14,
          },
          callback: (value) => (+value >= 1000 ? +value / 1000 + 'K' : value),
        },
      },
    },
    plugins: {
      legend: {
        display: false, // إخفاء اللِيجند
      },
      datalabels: {
        display: false, // إخفاء القيم فوق الأعمدة
      },
      tooltip: {
        enabled: true,
      },
    },
  };

  public barChartData: ChartData<'bar'> = {
    labels: [],
    datasets: [
      {
        label: 'عدد الطلبات',

        data: [],
        backgroundColor: '#127453', // اللون الأخضر الداكن كما في الصورة
        borderRadius: 6, // زوايا دائرية
        barThickness: 30, // سمك العمود
      },
    ],
  };
}
