import { Component } from '@angular/core';
import {ClickAndEatApiService} from "../../services/click-and-eat-api.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ErrorSnackbarComponent} from "../../snackbars/error-snackbar/error-snackbar.component";
import {SuccessSnackbarComponent} from "../../snackbars/success-snackbar/success-snackbar.component";
import {FoodTypeDto} from "../../dtos/food-type.dto";
import {Router} from "@angular/router";
import {MenuDto} from "../../dtos/menu.dto";
import {OrderLockState} from "../../utils/consts";
import {take} from "rxjs";

@Component({
  selector: 'app-order-view',
  templateUrl: './order-view.component.html',
  styleUrls: ['./order-view.component.scss']
})
export class OrderViewComponent {
  orderedItems: FoodTypeDto[];
  orderExists = true;
  menu: MenuDto;

  constructor(private router: Router,
              private clickAndEatApiService: ClickAndEatApiService,
              private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.clickAndEatApiService.getOrder().pipe(take(1)).subscribe({
      next: order => {
        if (order != null) {
          this.orderedItems = order.OrderedItems;
        } else {
          this.orderedItems = [];
          this.orderExists = false;
        }
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

  cancelOrder() {
    this.clickAndEatApiService.cancelOrder().pipe(take(1)).subscribe({
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

  get menuLocked(): boolean {
    return this.menu.OrderLockState == OrderLockState.Locked;
  }
}
