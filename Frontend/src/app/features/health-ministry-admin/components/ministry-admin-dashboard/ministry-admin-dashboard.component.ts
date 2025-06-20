import { Component, OnInit, ViewChild } from '@angular/core';
import { Chart, ChartConfiguration, ChartData, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { HealthMinistryService } from '../../services/health-ministry.service';
import Swal from 'sweetalert2';
import ChartDataLabels from 'chartjs-plugin-datalabels';
Chart.register(ChartDataLabels);

@Component({
  selector: 'app-ministry-admin-dashboard',
  standalone: false,
  templateUrl: './ministry-admin-dashboard.component.html',
  styleUrl: './ministry-admin-dashboard.component.css',
})
export class MinistryAdminDashboardComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;
  @ViewChild(BaseChartDirective) chartBar:
    | BaseChartDirective<'bar'>
    | undefined;
  public pieChartType: ChartType = 'pie';

  data: any = {};

  constructor(private _HealthMinistryService: HealthMinistryService) {}
  ngOnInit(): void {
    this.getStatistics();
  }
  getStatistics() {
    this._HealthMinistryService.getStatisticsOfAdmin().subscribe({
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

        const labels1 = this.data.vaccineRequests.map(
          (item: any) => item.vaccineName
        );
        const data1 = this.data.vaccineRequests.map(
          (item: any) => item.requestedAmount
        );
        this.barChartData1.labels = labels1;
        this.barChartData1.datasets[0].data = data1;
        this.barChartData1 = { ...this.barChartData1 };

        const labels2 = this.data.topGovernoratesByVaccines.map(
          (item: any) => item.governorate
        );
        const data2 = this.data.topGovernoratesByVaccines.map(
          (item: any) => item.totalVaccinesRequested
        );
        this.barChartData2.labels = labels2;
        this.barChartData2.datasets[0].data = data2;
        this.barChartData2 = { ...this.barChartData2 };
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
        backgroundColor: ['#00ACF8', '#ec4899'],
      },
    ],
  };
  //--------------------------------- bar---------------------------------
  // first bar
  public barChartType = 'bar' as const;

  public barChartOptions1: ChartConfiguration<'bar'>['options'] = {
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

  public barChartData1: ChartData<'bar'> = {
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

  //second bar
  public barChartOptions2: ChartConfiguration<'bar'>['options'] = {
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

  public barChartData2: ChartData<'bar'> = {
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
