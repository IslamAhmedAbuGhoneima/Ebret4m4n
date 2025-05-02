import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinatedChildrenComponent } from './vaccinated-children.component';

describe('VaccinatedChildrenComponent', () => {
  let component: VaccinatedChildrenComponent;
  let fixture: ComponentFixture<VaccinatedChildrenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VaccinatedChildrenComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VaccinatedChildrenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
