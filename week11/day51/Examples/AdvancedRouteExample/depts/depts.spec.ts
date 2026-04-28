import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Depts } from './depts';

describe('Depts', () => {
  let component: Depts;
  let fixture: ComponentFixture<Depts>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Depts],
    }).compileComponents();

    fixture = TestBed.createComponent(Depts);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
