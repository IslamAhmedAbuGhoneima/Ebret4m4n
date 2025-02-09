import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddChildPageComponent } from './add-child-page.component';

describe('AddChildPageComponent', () => {
  let component: AddChildPageComponent;
  let fixture: ComponentFixture<AddChildPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddChildPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddChildPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
