import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityCenterAdminDashboardComponent } from './city-center-admin-dashboard.component';

describe('CityCenterAdminDashboardComponent', () => {
  let component: CityCenterAdminDashboardComponent;
  let fixture: ComponentFixture<CityCenterAdminDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CityCenterAdminDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CityCenterAdminDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
