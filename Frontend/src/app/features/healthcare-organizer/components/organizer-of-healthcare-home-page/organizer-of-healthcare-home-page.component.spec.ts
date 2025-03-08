import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizerOfHealthcareHomePageComponent } from './organizer-of-healthcare-home-page.component';

describe('OrganizerOfHealthcareHomePageComponent', () => {
  let component: OrganizerOfHealthcareHomePageComponent;
  let fixture: ComponentFixture<OrganizerOfHealthcareHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrganizerOfHealthcareHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizerOfHealthcareHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
