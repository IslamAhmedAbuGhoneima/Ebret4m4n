import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthUnitComponent } from './health-unit.component';

describe('HealthUnitComponent', () => {
  let component: HealthUnitComponent;
  let fixture: ComponentFixture<HealthUnitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HealthUnitComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HealthUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
