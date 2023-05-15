import {Component, OnInit} from '@angular/core';
import {FormControl, Validators} from "@angular/forms";
import {FormErrorChecker} from "../../utils/form.error.checker";
import {ClickAndEatApiService} from "../../services/click-and-eat-api.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {SuccessSnackbarComponent} from "../../snackbars/success-snackbar/success-snackbar.component";
import {ErrorSnackbarComponent} from "../../snackbars/error-snackbar/error-snackbar.component";
import {ActivatedRoute} from "@angular/router";
import {FoodTypeEditRequestBuilder} from "../../builders/food-type-edit.request.builder";
import {take} from "rxjs";

@Component({
  selector: 'app-food-type-edit',
  templateUrl: './food-type-edit.component.html',
  styleUrls: ['./food-type-edit.component.scss']
})
export class FoodTypeEditComponent implements OnInit{
  identifier: string;
  type = new FormControl('', [Validators.required]);
  description = new FormControl('', [Validators.required]);
  imageLink = new FormControl('', [Validators.required]);
  checker = new FormErrorChecker();

  constructor(private clickAndEatApiService: ClickAndEatApiService,
              private route: ActivatedRoute,
              private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.identifier = params['identifier']
    })

    this.clickAndEatApiService.getFoodType(this.identifier).pipe(take(1)).subscribe({
      next: foodType => {
        this.type.setValue(foodType.Type);
        this.description.setValue(foodType.Description);
        this.imageLink.setValue(foodType.ImageLink);
      },
      error: _ => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    });
  }

  editFoodType() {
    const foodTypeEditRequest = FoodTypeEditRequestBuilder.init().aFoodTypeEditRequest(this.identifier)
      .withType(this.type.value ?? '')
      .withDescription(this.description.value ?? '')
      .withImageLink(this.imageLink.value ?? '')
      .build();

    this.clickAndEatApiService.updateFoodType(foodTypeEditRequest).pipe(take(1)).subscribe({
      next: _ => {
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

  get submitIsDisabled(): boolean {
    return this.type.invalid || this.description.invalid || this.imageLink.invalid;
  }
}
