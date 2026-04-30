import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Emps } from './emps';

describe('Emps', () => {
  let component: Emps;
  let fixture: ComponentFixture<Emps>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Emps],
    }).compileComponents();

    fixture = TestBed.createComponent(Emps);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
