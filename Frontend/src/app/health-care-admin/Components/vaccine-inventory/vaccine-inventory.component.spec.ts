import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccineInventoryComponent } from './vaccine-inventory.component';

describe('VaccineInventoryComponent', () => {
  let component: VaccineInventoryComponent;
  let fixture: ComponentFixture<VaccineInventoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VaccineInventoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VaccineInventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
