import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinationBookingComponent } from './vaccination-booking.component';

describe('VaccinationBookingComponent', () => {
  let component: VaccinationBookingComponent;
  let fixture: ComponentFixture<VaccinationBookingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VaccinationBookingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VaccinationBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
