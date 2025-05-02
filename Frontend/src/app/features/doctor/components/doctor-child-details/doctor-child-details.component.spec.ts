import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorChildDetailsComponent } from './doctor-child-details.component';

describe('DoctorChildDetailsComponent', () => {
  let component: DoctorChildDetailsComponent;
  let fixture: ComponentFixture<DoctorChildDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DoctorChildDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorChildDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
