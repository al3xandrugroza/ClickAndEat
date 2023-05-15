import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodTypeEditComponent } from './food-type-edit.component';

describe('FoodTypeEditComponent', () => {
  let component: FoodTypeEditComponent;
  let fixture: ComponentFixture<FoodTypeEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoodTypeEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodTypeEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
