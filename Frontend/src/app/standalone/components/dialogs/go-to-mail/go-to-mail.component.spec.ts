import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoToMailComponent } from './go-to-mail.component';

describe('GoToMailComponent', () => {
  let component: GoToMailComponent;
  let fixture: ComponentFixture<GoToMailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GoToMailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GoToMailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
