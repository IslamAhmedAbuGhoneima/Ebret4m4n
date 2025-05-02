import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentProfileEditComponent } from './parent-profile-edit.component';

describe('ParentProfileEditComponent', () => {
  let component: ParentProfileEditComponent;
  let fixture: ComponentFixture<ParentProfileEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParentProfileEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParentProfileEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
