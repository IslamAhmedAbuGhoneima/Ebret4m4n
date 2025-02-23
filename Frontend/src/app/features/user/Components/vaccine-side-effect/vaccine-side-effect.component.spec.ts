import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccineSideEffectComponent } from './vaccine-side-effect.component';

describe('VaccineSideEffectComponent', () => {
  let component: VaccineSideEffectComponent;
  let fixture: ComponentFixture<VaccineSideEffectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VaccineSideEffectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VaccineSideEffectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
