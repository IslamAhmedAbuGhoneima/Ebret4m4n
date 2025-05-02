import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyChildrenHomePageComponent } from './my-children-home-page.component';

describe('MyChildrenHomePageComponent', () => {
  let component: MyChildrenHomePageComponent;
  let fixture: ComponentFixture<MyChildrenHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyChildrenHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyChildrenHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
