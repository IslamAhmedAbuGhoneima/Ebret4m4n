import { Component, OnInit } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NotificationService } from './core/services/notification.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MatSidenavModule, NgxSpinnerModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Frontend';

  constructor(
    private _NotificationService: NotificationService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
   
    this._NotificationService.requestPermission();
  }
}
