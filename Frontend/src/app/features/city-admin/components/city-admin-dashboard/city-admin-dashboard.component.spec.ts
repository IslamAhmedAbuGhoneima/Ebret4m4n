import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityAdminDashboardComponent } from './city-admin-dashboard.component';

describe('CityAdminDashboardComponent', () => {
  let component: CityAdminDashboardComponent;
  let fixture: ComponentFixture<CityAdminDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CityAdminDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CityAdminDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
