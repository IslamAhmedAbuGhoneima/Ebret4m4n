import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthMinistryAdminHomePageComponent } from './health-ministry-admin-home-page.component';

describe('HealthMinistryAdminHomePageComponent', () => {
  let component: HealthMinistryAdminHomePageComponent;
  let fixture: ComponentFixture<HealthMinistryAdminHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HealthMinistryAdminHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HealthMinistryAdminHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
