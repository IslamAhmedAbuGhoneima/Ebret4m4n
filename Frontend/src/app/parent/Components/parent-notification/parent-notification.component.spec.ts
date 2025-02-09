import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentNotificationComponent } from './parent-notification.component';

describe('ParentNotificationComponent', () => {
  let component: ParentNotificationComponent;
  let fixture: ComponentFixture<ParentNotificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParentNotificationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParentNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
