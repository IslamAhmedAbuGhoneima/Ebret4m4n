import { TestBed } from '@angular/core/testing';

import { HealthMinistryService } from './health-ministry.service';

describe('HealthMinistryService', () => {
  let service: HealthMinistryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthMinistryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
