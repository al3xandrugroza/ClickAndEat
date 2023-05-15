import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingCartEditComponent } from './shopping-cart-edit.component';

describe('ShoppingCartEditComponent', () => {
  let component: ShoppingCartEditComponent;
  let fixture: ComponentFixture<ShoppingCartEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingCartEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShoppingCartEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
