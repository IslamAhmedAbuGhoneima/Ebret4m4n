import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MinistryAdminDashboardComponent } from './ministry-admin-dashboard.component';

describe('MinistryAdminDashboardComponent', () => {
  let component: MinistryAdminDashboardComponent;
  let fixture: ComponentFixture<MinistryAdminDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MinistryAdminDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MinistryAdminDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
