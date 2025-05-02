import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorsHomePageComponent } from './doctors-home-page.component';

describe('DoctorsHomePageComponent', () => {
  let component: DoctorsHomePageComponent;
  let fixture: ComponentFixture<DoctorsHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DoctorsHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorsHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
