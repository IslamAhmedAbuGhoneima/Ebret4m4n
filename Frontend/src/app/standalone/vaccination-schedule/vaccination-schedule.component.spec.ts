import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinationScheduleComponent } from './vaccination-schedule.component';

describe('VaccinationScheduleComponent', () => {
  let component: VaccinationScheduleComponent;
  let fixture: ComponentFixture<VaccinationScheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VaccinationScheduleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VaccinationScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
