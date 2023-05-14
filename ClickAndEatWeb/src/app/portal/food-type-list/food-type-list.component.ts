import {Component, OnInit} from '@angular/core';
import {MatSnackBar} from "@angular/material/snack-bar";
import {ClickAndEatApiService} from "../../services/click-and-eat-api.service";
import {FoodTypeDto} from "../../dtos/food-type.dto";
import {ErrorSnackbarComponent} from "../../snackbars/error-snackbar/error-snackbar.component";
import {take} from "rxjs";

@Component({
  selector: 'app-food-type-list',
  templateUrl: './food-type-list.component.html',
  styleUrls: ['./food-type-list.component.scss']
})
export class FoodTypeListComponent implements OnInit{
  foodTypes: FoodTypeDto[];

  constructor(private clickAndEatApiService: ClickAndEatApiService, private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.clickAndEatApiService.getAllFoodTypes().pipe(take(1)).subscribe({
      next: foodTypes => {
        this.foodTypes = foodTypes
      },
      error: _ => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    });
  }
}
