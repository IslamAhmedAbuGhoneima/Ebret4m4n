import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideEffectsComponent } from './side-effects.component';

describe('SideEffectsComponent', () => {
  let component: SideEffectsComponent;
  let fixture: ComponentFixture<SideEffectsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SideEffectsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SideEffectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
