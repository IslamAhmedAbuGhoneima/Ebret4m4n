import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildVaccinationScheduleComponent } from './child-vaccination-schedule.component';

describe('ChildVaccinationScheduleComponent', () => {
  let component: ChildVaccinationScheduleComponent;
  let fixture: ComponentFixture<ChildVaccinationScheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChildVaccinationScheduleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChildVaccinationScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
