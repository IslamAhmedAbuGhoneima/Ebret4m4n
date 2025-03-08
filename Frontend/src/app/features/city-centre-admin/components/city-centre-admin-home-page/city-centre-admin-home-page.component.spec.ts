import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityCentreAdminHomePageComponent } from './city-centre-admin-home-page.component';

describe('CityCentreAdminHomePageComponent', () => {
  let component: CityCentreAdminHomePageComponent;
  let fixture: ComponentFixture<CityCentreAdminHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CityCentreAdminHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CityCentreAdminHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
