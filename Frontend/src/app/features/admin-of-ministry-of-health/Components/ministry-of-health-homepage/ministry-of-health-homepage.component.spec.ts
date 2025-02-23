import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MinistryOfHealthHomepageComponent } from './ministry-of-health-homepage.component';

describe('MinistryOfHealthHomepageComponent', () => {
  let component: MinistryOfHealthHomepageComponent;
  let fixture: ComponentFixture<MinistryOfHealthHomepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MinistryOfHealthHomepageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MinistryOfHealthHomepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
