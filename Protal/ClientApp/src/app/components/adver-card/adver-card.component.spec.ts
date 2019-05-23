import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdverCardComponent } from './adver-card.component';

describe('AdverCardComponent', () => {
  let component: AdverCardComponent;
  let fixture: ComponentFixture<AdverCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdverCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdverCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
