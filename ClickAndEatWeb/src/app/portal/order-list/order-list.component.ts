import {Component, OnInit} from '@angular/core';
import {ClickAndEatApiService} from "../../services/click-and-eat-api.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MenuDto} from "../../dtos/menu.dto";
import {ErrorSnackbarComponent} from "../../snackbars/error-snackbar/error-snackbar.component";
import {OrderSummaryDto} from "../../dtos/order-summary.dto";
import {SuccessSnackbarComponent} from "../../snackbars/success-snackbar/success-snackbar.component";
import {Router} from "@angular/router";
import {OrderLockState} from "../../utils/consts";
import {take} from "rxjs";

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit {
  menu: MenuDto;
  allOrders: OrderSummaryDto[];

  displayedColumns: string[] = ['Email', 'FoodTypes'];

  constructor(private clickAndEatApiService: ClickAndEatApiService,
              private snackBar: MatSnackBar,
              private router: Router) {}

  ngOnInit(): void {
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

    this.clickAndEatApiService.getAllOrders().pipe(take(1)).subscribe({
      next: allOrders => {
        this.allOrders = allOrders;
      },
      error: _ => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    });
  }

  deleteAllOrders() {
    this.clickAndEatApiService.deleteAllOrders().pipe(take(1)).subscribe({
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

  get ordersListIsEmpty(): boolean {
    return this.allOrders.length == 0;
  }

  get menuIsUnlocked(): boolean {
    return this.menu.OrderLockState == OrderLockState.Unlocked;
  }
}
