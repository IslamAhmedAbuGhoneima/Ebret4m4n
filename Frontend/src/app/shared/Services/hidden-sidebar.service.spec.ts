import { TestBed } from '@angular/core/testing';

import { HiddenSidebarService } from './hidden-sidebar.service';

describe('HiddenSidebarService', () => {
  let service: HiddenSidebarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HiddenSidebarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
