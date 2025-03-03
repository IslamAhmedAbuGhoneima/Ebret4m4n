import { Component, OnInit } from '@angular/core';
import { slideInOut } from '../../Animations/sidebar-transition';
import { HiddenSidebarService } from '../../Services/hidden-sidebar.service';

@Component({
  selector: 'app-navbar',
  standalone: false,
  animations: [slideInOut],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})

export class NavbarComponent {
  navbarState: string = 'hidden';
  constructor(private hiddenSidebarService: HiddenSidebarService) {
  }
  toggleNavbar() {
    this.hiddenSidebarService.sidebarState$.subscribe(res => {
      this.navbarState = res
    })
    this.navbarState = this.navbarState === 'hidden' ? 'visible' : 'hidden';
    this.hiddenSidebarService.setSidebar(this.navbarState)
  }
}
