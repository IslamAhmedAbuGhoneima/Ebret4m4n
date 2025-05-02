import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CentersHomePageComponent } from './centers-home-page.component';

describe('CentersHomePageComponent', () => {
  let component: CentersHomePageComponent;
  let fixture: ComponentFixture<CentersHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CentersHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CentersHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
