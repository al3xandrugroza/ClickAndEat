import {Component, inject} from '@angular/core';
import {MatSnackBarRef} from "@angular/material/snack-bar";

@Component({
  selector: 'app-bad-credentials-snackbar',
  templateUrl: './bad-credentials-snackbar.component.html',
  styleUrls: ['./bad-credentials-snackbar.component.scss']
})
export class BadCredentialsSnackbarComponent {
  snackBarRef = inject(MatSnackBarRef);
}
