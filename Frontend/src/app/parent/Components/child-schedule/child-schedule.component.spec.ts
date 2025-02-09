import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildScheduleComponent } from './child-schedule.component';

describe('ChildScheduleComponent', () => {
  let component: ChildScheduleComponent;
  let fixture: ComponentFixture<ChildScheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChildScheduleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChildScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
