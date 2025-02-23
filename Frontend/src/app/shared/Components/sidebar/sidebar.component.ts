import { Component } from '@angular/core';
import { slideInOut } from '../../Animations/sidebar-transition';
import { BehaviorSubject } from 'rxjs';
import { HiddenSidebarService } from '../../Services/hidden-sidebar.service';


@Component({
  selector: 'app-sidebar',
  standalone: false,
  animations: [slideInOut],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
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
