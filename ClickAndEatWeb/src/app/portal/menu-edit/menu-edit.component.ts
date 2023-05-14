import { Component } from '@angular/core';
import {ErrorSnackbarComponent} from "../../snackbars/error-snackbar/error-snackbar.component";
import {FoodTypeDto} from "../../dtos/food-type.dto";
import {ClickAndEatApiService} from "../../services/click-and-eat-api.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MenuDto} from "../../dtos/menu.dto";
import {MenuUpdateRequestBuilder} from "../../builders/menu-update.request.builder";
import {SuccessSnackbarComponent} from "../../snackbars/success-snackbar/success-snackbar.component";
import {OrderLockState} from "../../utils/consts";
import {take} from "rxjs";

@Component({
  selector: 'app-menu-edit',
  templateUrl: './menu-edit.component.html',
  styleUrls: ['./menu-edit.component.scss']
})
export class MenuEditComponent {
  allFoodTypes: FoodTypeDto[];
  menu: MenuDto;

  constructor(private clickAndEatApiService: ClickAndEatApiService, private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.clickAndEatApiService.getAllFoodTypes().pipe(take(1)).subscribe({
      next: foodTypes => {
        this.allFoodTypes = foodTypes
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

  updateMenu() {
    const menuUpdateRequestDto = MenuUpdateRequestBuilder.init().aMenuUpdateRequest().withChoiceList(this.menu.ChoiceList).build();
    this.clickAndEatApiService.updateMenu(menuUpdateRequestDto).pipe(take(1)).subscribe({
      next: menu => {
        this.snackBar.openFromComponent(SuccessSnackbarComponent, {
          duration: 1.5 * 1000
        });

        this.menu = menu;
      },
      error: _ => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    });
  }

  lockMenu() {
    this.clickAndEatApiService.lockMenu().pipe(take(1)).subscribe({
      next: _ => {
        this.menu.OrderLockState = OrderLockState.Locked;
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

  unlockMenu() {
    this.clickAndEatApiService.unlockMenu().pipe(take(1)).subscribe({
      next: _ => {
        this.menu.OrderLockState = OrderLockState.Unlocked;
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

  toggleFromChoiceList(givenFoodType: FoodTypeDto) {
    const index = this.menu.ChoiceList.findIndex((ft) => ft.Identifier === givenFoodType.Identifier);
    if (index === -1) {
      this.menu.ChoiceList.push(givenFoodType);
      return;
    }

    this.menu.ChoiceList.splice(index, 1);
  }

  isSelected(givenFoodType: FoodTypeDto): boolean {
    const index = this.menu.ChoiceList.findIndex((ft) => ft.Identifier === givenFoodType.Identifier);
    return index != -1;
  }

  isLocked(): boolean {
    return this.menu.OrderLockState === OrderLockState.Locked;
  }

  isUnlocked(): boolean {
    return this.menu.OrderLockState === OrderLockState.Unlocked;
  }
}
