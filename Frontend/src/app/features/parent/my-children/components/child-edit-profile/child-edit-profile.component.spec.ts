import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildEditProfileComponent } from './child-edit-profile.component';

describe('ChildEditProfileComponent', () => {
  let component: ChildEditProfileComponent;
  let fixture: ComponentFixture<ChildEditProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChildEditProfileComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChildEditProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
