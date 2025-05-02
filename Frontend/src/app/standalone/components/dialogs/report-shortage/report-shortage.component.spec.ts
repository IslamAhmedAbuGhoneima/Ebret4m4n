import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportShortageComponent } from './report-shortage.component';

describe('ReportShortageComponent', () => {
  let component: ReportShortageComponent;
  let fixture: ComponentFixture<ReportShortageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReportShortageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReportShortageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
