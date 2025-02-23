import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthCareAdminHomePageComponent } from './health-care-admin-home-page.component';

describe('HealthCareAdminHomePageComponent', () => {
  let component: HealthCareAdminHomePageComponent;
  let fixture: ComponentFixture<HealthCareAdminHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HealthCareAdminHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HealthCareAdminHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
