import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdverDetailComponent } from './adver-detail.component';

describe('AdverDetailComponent', () => {
  let component: AdverDetailComponent;
  let fixture: ComponentFixture<AdverDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdverDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdverDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
