import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitsHomePageComponent } from './units-home-page.component';

describe('UnitsHomePageComponent', () => {
  let component: UnitsHomePageComponent;
  let fixture: ComponentFixture<UnitsHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UnitsHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnitsHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
