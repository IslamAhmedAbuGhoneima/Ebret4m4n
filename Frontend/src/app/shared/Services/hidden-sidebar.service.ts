import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HiddenSidebarService {
  sidebarState$ = new BehaviorSubject('hidden');
  constructor() { }
  setSidebar(content: string) {
    this.sidebarState$.next(content);
  }
}
