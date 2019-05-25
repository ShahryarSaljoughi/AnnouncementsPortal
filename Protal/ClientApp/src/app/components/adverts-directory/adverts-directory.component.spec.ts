import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertsDirectoryComponent } from './adverts-directory.component';

describe('AdvertsDirectoryComponent', () => {
  let component: AdvertsDirectoryComponent;
  let fixture: ComponentFixture<AdvertsDirectoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdvertsDirectoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvertsDirectoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
