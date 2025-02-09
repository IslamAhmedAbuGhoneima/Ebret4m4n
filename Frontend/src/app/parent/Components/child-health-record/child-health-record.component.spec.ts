import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildHealthRecordComponent } from './child-health-record.component';

describe('ChildHealthRecordComponent', () => {
  let component: ChildHealthRecordComponent;
  let fixture: ComponentFixture<ChildHealthRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChildHealthRecordComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChildHealthRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
