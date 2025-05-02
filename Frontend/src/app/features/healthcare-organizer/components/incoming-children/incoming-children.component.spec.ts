import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomingChildrenComponent } from './incoming-children.component';

describe('IncomingChildrenComponent', () => {
  let component: IncomingChildrenComponent;
  let fixture: ComponentFixture<IncomingChildrenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IncomingChildrenComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncomingChildrenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
