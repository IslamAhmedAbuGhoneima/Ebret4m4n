import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgIconsModule.withIcons({ heroUser, heroBell, heroChatBubbleOvalLeft, heroBars3Mini }),
  ],
  exports: [],
})
export class SharedModule { }
