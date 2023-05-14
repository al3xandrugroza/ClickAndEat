import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ActivatedRoute} from "@angular/router";
import {LoginRequest} from "../dtos/login.request";
import {
  BadCredentialsSnackbarComponent
} from "../snackbars/bad-credentials-snackbar/bad-credentials-snackbar.component";
import {ConnectService} from "../services/connect.service";
import {take} from "rxjs";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent{
  email = new FormControl('', [Validators.required]);
  password = new FormControl('', [Validators.required]);

  constructor(private formBuilder: FormBuilder,
              private http: HttpClient,
              private connectServiceService: ConnectService,
              private snackBar: MatSnackBar) {}

  onSubmit() {
    const loginRequest = new LoginRequest();
    loginRequest.UserName = this.email.value ?? '';
    loginRequest.Password = this.password.value ?? '';

    this.connectServiceService.login(loginRequest).pipe(take(1)).subscribe({
      next: loginResponse => {
        window.location.href = loginResponse.returnUrl;
      },
      error: () => {
        this.email.reset();
        this.password.reset();

        this.snackBar.openFromComponent(BadCredentialsSnackbarComponent, {
          duration: 1.5 * 1000
        });
      }
    })
  }
}
