import {Component, Input, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ClickAndEatApiService} from "../../services/click-and-eat-api.service";
import {SuccessSnackbarComponent} from "../../snackbars/success-snackbar/success-snackbar.component";
import {ErrorSnackbarComponent} from "../../snackbars/error-snackbar/error-snackbar.component";
import {MenuDto} from "../../dtos/menu.dto";
import {OrderLockState} from "../../utils/consts";
import {take} from "rxjs";

@Component({
  selector: 'app-food-type',
  templateUrl: './food-type.component.html',
  styleUrls: ['./food-type.component.scss']
})
export class FoodTypeComponent implements OnInit {
  @Input() identifier: string;
  @Input() type: string;
  @Input() description: string;
  @Input() imageLink: string;
  @Input() hideButtons = false;

  deletedFoodType = false;
  menu: MenuDto;

  constructor(private router: Router,
              private snackBar: MatSnackBar,
              private clickAndEatApiService: ClickAndEatApiService) {
  }

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
  }

  editFoodType() {
    this.router.navigate(['/dashboard/foodType/edit', { identifier: this.identifier }])
  }

  deleteFoodType() {
    this.clickAndEatApiService.deleteFoodType(this.identifier).pipe(take(1)).subscribe({
      next: _ => {
        this.markAsDeleted();

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

  markAsDeleted() {
    this.deletedFoodType = true;
  }

  get menuIsUnlocked(): boolean {
    return this.menu.OrderLockState == OrderLockState.Unlocked;
  }
}
