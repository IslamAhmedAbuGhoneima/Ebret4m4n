import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinationEditComponent } from './vaccination-edit.component';

describe('VaccinationEditComponent', () => {
  let component: VaccinationEditComponent;
  let fixture: ComponentFixture<VaccinationEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VaccinationEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VaccinationEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
