/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GovernorateAdminService } from './governorateAdmin.service';

describe('Service: GovernorateAdmin', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GovernorateAdminService]
    });
  });

  it('should ...', inject([GovernorateAdminService], (service: GovernorateAdminService) => {
    expect(service).toBeTruthy();
  }));
});
