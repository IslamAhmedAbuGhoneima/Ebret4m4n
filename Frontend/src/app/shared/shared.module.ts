import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NgIconsModule } from '@ng-icons/core';
import {
  heroUser,
  heroBell,
  heroChatBubbleOvalLeft
} from '@ng-icons/heroicons/outline'
import {
  heroBars3Mini
} from '@ng-icons/heroicons/mini';


import { SidebarComponent } from './Components/sidebar/sidebar.component';
import { FooterComponent } from './Components/footer/footer.component';

@NgModule({
  declarations: [NavbarComponent, SidebarComponent, FooterComponent],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgIconsModule.withIcons({ heroUser, heroBell, heroChatBubbleOvalLeft, heroBars3Mini }),
  ],
  exports: [NavbarComponent, SidebarComponent, FooterComponent],
})
export class SharedModule { }
