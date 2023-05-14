import { Component } from '@angular/core';
import {FoodTypeDto} from "../../dtos/food-type.dto";
import {MenuDto} from "../../dtos/menu.dto";
import {ClickAndEatApiService} from "../../services/click-and-eat-api.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ErrorSnackbarComponent} from "../../snackbars/error-snackbar/error-snackbar.component";
import {SuccessSnackbarComponent} from "../../snackbars/success-snackbar/success-snackbar.component";
import {OrderLockState} from "../../utils/consts";
import {ShoppingCartDto} from "../../dtos/shopping-cart.dto";
import {ShoppingCartDtoBuilder} from "../../builders/shopping-cart.dto.builder";
import {OrderDto} from "../../dtos/order.dto";
import {take} from "rxjs";

@Component({
  selector: 'app-shopping-cart-edit',
  templateUrl: './shopping-cart-edit.component.html',
  styleUrls: ['./shopping-cart-edit.component.scss']
})
export class ShoppingCartEditComponent {
  shoppingCart: ShoppingCartDto;
  menu: MenuDto;
  order: OrderDto | null;

  constructor(private clickAndEatApiService: ClickAndEatApiService, private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.clickAndEatApiService.getShoppingCart().pipe(take(1)).subscribe({
      next: shoppingCart => {
        this.shoppingCart = shoppingCart
      },
      error: _ => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    });

    this.clickAndEatApiService.getMenu().pipe(take(1)).subscribe({
      next: menu => {
        this.menu = menu;
      },
      error: _ => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    });

    this.clickAndEatApiService.getOrder().pipe(take(1)).subscribe({
      next: order => {
        this.order = order;
      },
      error: _ => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    });
  }

  updateShoppingCart() {
    const shoppingCartDto = ShoppingCartDtoBuilder.init().aShoppingCartDto().withCartItems(this.shoppingCart.CartItems).build();
    this.clickAndEatApiService.updateShoppingCart(shoppingCartDto).pipe(take(1)).subscribe({
      next: shoppingCart => {
        this.shoppingCart = shoppingCart;
        this.snackBar.openFromComponent(SuccessSnackbarComponent, {
          duration: 1.5 * 1000
        })
      },
      error: _ => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    });
  }

  toggleFromCartItemsList(givenFoodType: FoodTypeDto) {
    const index = this.shoppingCart.CartItems.findIndex((ft) => ft.Identifier === givenFoodType.Identifier);
    if (index === -1) {
      this.shoppingCart.CartItems.push(givenFoodType);
      return;
    }

    this.shoppingCart.CartItems.splice(index, 1);
  }

  isSelected(givenFoodType: FoodTypeDto): boolean {
    const index = this.shoppingCart.CartItems.findIndex((ft) => ft.Identifier === givenFoodType.Identifier);
    return index != -1;
  }

  get orderAlreadyExists(): boolean {
    return this.order != null;
  }

  get menuLocked(): boolean {
    return this.menu.OrderLockState == OrderLockState.Locked;
  }
}
