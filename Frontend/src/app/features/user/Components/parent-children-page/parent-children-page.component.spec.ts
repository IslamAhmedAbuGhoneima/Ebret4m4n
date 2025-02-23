import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentChildrenPageComponent } from './parent-children-page.component';

describe('ParentChildrenPageComponent', () => {
  let component: ParentChildrenPageComponent;
  let fixture: ComponentFixture<ParentChildrenPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParentChildrenPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParentChildrenPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
