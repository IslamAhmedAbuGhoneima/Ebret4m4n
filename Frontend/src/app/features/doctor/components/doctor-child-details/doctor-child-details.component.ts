import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-doctor-child-details',
  standalone: false,
  templateUrl: './doctor-child-details.component.html',
  styleUrl: './doctor-child-details.component.css',
})
export class DoctorChildDetailsComponent implements OnInit {
  fromPage: string = '';
  imageList = [
    {
      name: 'صورة 1',
      url: 'https://www.google.com/imgres?imgurl=https://ichef.bbci.co.uk/ace/ws/640/cpsprodpb/16872/production/_128047229_49852da7-9242-4432-ab6b-97550dd0efa8.jpg.webp&tbnid=Ifdg03i06DfQpM&vet=1&imgrefurl=https://www.bbc.com/arabic/features-64021656&docid=DLYrRa4Bed_xLM&w=640&h=360&source=sh/x/im/m5/2&kgs=d45a01ea27c4a3cc',
    },
    {
      name: 'صورة 2',
      url: 'https://www.google.com/imgres?imgurl=https://media.filfan.com/NewsPics/FilfanNew/large/324969_0.jpg&tbnid=Cf64epNwWn55iM&vet=1&imgrefurl=https://www.filfan.com/news/130088&docid=BFbAuCFUE5r3IM&w=408&h=510&source=sh/x/im/m1/2&kgs=0259dccfaf7101d8',
    },
  ];
  deferredChild: boolean = true;
  constructor(private route: ActivatedRoute, private location: Location) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.fromPage = params['from']; // القيمة الافتراضية
    });
  }
  goBack() {
    this.location.back();
  }
}
