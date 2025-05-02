import { Component, ViewChild } from '@angular/core';
import { ChartConfiguration } from 'chart.js';
import { ChartData, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { Chart } from 'chart.js';

// تسجيل الـ plugins
Chart.register(ChartDataLabels);
@Component({
  selector: 'app-city-admin-dashboard',
  standalone: false,
  templateUrl: './city-admin-dashboard.component.html',
  styleUrl: './city-admin-dashboard.component.css',
})
export class CityAdminDashboardComponent {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;
  @ViewChild(BaseChartDirective) chartBar:
    | BaseChartDirective<'bar'>
    | undefined;

  // Pie
  public pieChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
        position: 'bottom',
        labels: {
          padding: 20,
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
          size: 20,
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
    labels: ['ذكر', 'أنثى'],
    datasets: [
      {
        data: [300, 500],
        backgroundColor: ['#00ACF8', '#ec4899'], // 💙 ذكر، 💗 أنثى
      },
    ],
  };
  public pieChartType: ChartType = 'pie';

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
    labels: [
      'القاهرة',
      'الجيزة',
      'الإسكندرية',
      'البحيرة',
      'الفيوم',
      'الغربية',
      'الإسماعيلية',
      'المنوفية',
      'المنيا',
      'السويس',
      'أسوان',
      'أسيوط',
      'بني سويف',
      'بورسعيد',
      'دمياط',
 
      'مطروح',
      'الأقصر',
      'قنا',

    ],
    datasets: [
      {
        label: 'عدد الطلبات',
        data: [
          320000, 180000, 370000, 260000, 120000, 210000, 20000, 210000,
          220000, 210000, 200000, 190000, 180000, 180000, 14000, 180000,
          15000, 40000,
        ],
        backgroundColor: '#127453', // اللون الأخضر الداكن كما في الصورة
        borderRadius: 6, // زوايا دائرية
        barThickness: 30, // سمك العمود
      },
    ],
  };
}
