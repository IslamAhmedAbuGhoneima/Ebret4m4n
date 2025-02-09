import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentHomePageComponent } from './parent-home-page.component';

describe('ParentHomePageComponent', () => {
  let component: ParentHomePageComponent;
  let fixture: ComponentFixture<ParentHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParentHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParentHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
