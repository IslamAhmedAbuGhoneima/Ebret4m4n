import { Component } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { RouterOutlet } from '@angular/router';
import { DashboardComponent } from "./dashboard/dashboard.component";
import { FooterComponent } from './footer/footer.component';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, DashboardComponent, FooterComponent],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {

}
