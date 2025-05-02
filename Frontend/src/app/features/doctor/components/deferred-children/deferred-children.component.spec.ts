import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeferredChildrenComponent } from './deferred-children.component';

describe('DeferredChildrenComponent', () => {
  let component: DeferredChildrenComponent;
  let fixture: ComponentFixture<DeferredChildrenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeferredChildrenComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeferredChildrenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
