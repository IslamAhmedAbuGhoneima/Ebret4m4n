import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVaccinesComponent } from './add-vaccines.component';

describe('AddVaccinesComponent', () => {
  let component: AddVaccinesComponent;
  let fixture: ComponentFixture<AddVaccinesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddVaccinesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddVaccinesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
