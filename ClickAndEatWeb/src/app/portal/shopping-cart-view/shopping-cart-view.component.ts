import { Component } from '@angular/core';
import {ClickAndEatApiService} from "../../services/click-and-eat-api.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ErrorSnackbarComponent} from "../../snackbars/error-snackbar/error-snackbar.component";
import {ShoppingCartDto} from "../../dtos/shopping-cart.dto";
import {SuccessSnackbarComponent} from "../../snackbars/success-snackbar/success-snackbar.component";
import {OrderDto} from "../../dtos/order.dto";
import {Router} from "@angular/router";
import {OrderLockState} from "../../utils/consts";
import {MenuDto} from "../../dtos/menu.dto";
import {take} from "rxjs";

@Component({
  selector: 'app-shopping-cart-view',
  templateUrl: './shopping-cart-view.component.html',
  styleUrls: ['./shopping-cart-view.component.scss']
})
export class ShoppingCartViewComponent {
  shoppingCart: ShoppingCartDto;
  order: OrderDto | null;
  menu: MenuDto;

  constructor(private router: Router,
              private clickAndEatApiService: ClickAndEatApiService,
              private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.clickAndEatApiService.getShoppingCart().pipe(take(1)).subscribe({
      next: shoppingCart => {
        this.shoppingCart = shoppingCart;
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
  }

  placeOrder() {
    this.clickAndEatApiService.placeOrder().pipe(take(1)).subscribe({
      next: _ => {
        this.snackBar.openFromComponent(SuccessSnackbarComponent, {
          duration: 1.5 * 1000
        });

        window.location.href = window.location.origin;
      },
      error: _ => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    })
  }

  get orderAlreadyExists(): boolean {
    return this.order != null;
  }

  get emptyCart(): boolean {
    return !this.shoppingCart.CartItems || this.shoppingCart.CartItems.length == 0;
  }

  get menuLocked(): boolean {
    return this.menu.OrderLockState == OrderLockState.Locked;
  }
}
