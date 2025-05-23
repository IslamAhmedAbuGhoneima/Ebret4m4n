/* tslint:disable:no-unused-variable */

import { TestBed, inject } from '@angular/core/testing';
import { CityCenterService } from './cityCenter.service';

describe('Service: CityCenter', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CityCenterService]
    });
  });

  it('should ...', inject([CityCenterService], (service: CityCenterService) => {
    expect(service).toBeTruthy();
  }));
});
