import {Component, inject} from '@angular/core';
import {MatSnackBarRef} from "@angular/material/snack-bar";

@Component({
  selector: 'app-success-snackbar',
  templateUrl: './success-snackbar.component.html',
  styleUrls: ['./success-snackbar.component.scss']
})
export class SuccessSnackbarComponent {
  snackBarRef = inject(MatSnackBarRef);
}
