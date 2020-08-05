import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GetFavoritesComponent } from './get-favorites.component';

describe('GetFavoritesComponent', () => {
  let component: GetFavoritesComponent;
  let fixture: ComponentFixture<GetFavoritesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GetFavoritesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GetFavoritesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
