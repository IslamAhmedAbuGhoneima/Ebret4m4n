import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentProfileComponent } from './parent-profile.component';

describe('ParentProfileComponent', () => {
  let component: ParentProfileComponent;
  let fixture: ComponentFixture<ParentProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParentProfileComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParentProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
