import { Component } from '@angular/core';
import {FormControl, Validators} from "@angular/forms";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ErrorSnackbarComponent} from "../../snackbars/error-snackbar/error-snackbar.component";
import {SuccessSnackbarComponent} from "../../snackbars/success-snackbar/success-snackbar.component";
import {FoodTypeCreateRequestBuilder} from "../../builders/food-type-create.request.builder";
import {ClickAndEatApiService} from "../../services/click-and-eat-api.service";
import {take} from "rxjs";
import {FormErrorChecker} from "../../utils/form.error.checker";

@Component({
  selector: 'app-food-type-create',
  templateUrl: './food-type-create.component.html',
  styleUrls: ['./food-type-create.component.scss']
})
export class FoodTypeCreateComponent {
  type = new FormControl('', [Validators.required]);
  description = new FormControl('', [Validators.required]);
  imageLink = new FormControl('', [Validators.required]);
  checker = new FormErrorChecker();

  constructor(private clickAndEatApiService: ClickAndEatApiService, private snackBar: MatSnackBar) {}

  createFoodType() {
    const foodTypeCreateRequestDto = FoodTypeCreateRequestBuilder.init().aFoodTypeCreateRequestDto()
      .withType(this.type.value ?? '')
      .withDescription(this.description.value ?? '')
      .withImageLink(this.imageLink.value ?? '')
      .build();

    this.clickAndEatApiService.createFoodType(foodTypeCreateRequestDto).pipe(take(1)).subscribe({
      next: _ => {
        this.formReset();
        this.snackBar.openFromComponent(SuccessSnackbarComponent, {
          duration: 1.5 * 1000
        });
      },
      error: _ => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    });
  }

  private formReset() {
    this.type.reset();
    this.description.reset();
    this.imageLink.reset();
  }

  get submitIsDisabled(): boolean {
    return this.type.invalid || this.description.invalid || this.imageLink.invalid;
  }
}
