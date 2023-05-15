import { Component } from '@angular/core';
import {RegisterState} from "../utils/consts";
import {FormControl, Validators} from "@angular/forms";
import {passwordValidator} from "../utils/password-validator.directive";
import {FormErrorChecker} from "../utils/form.error.checker";
import {IdentityService} from "../services/identity.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpStatusCode} from "@angular/common/http";
import {ErrorSnackbarComponent} from "../snackbars/error-snackbar/error-snackbar.component";
import {RegisterOrganizationRequestDto} from "../dtos/register-organization.request.dto";
import {take} from "rxjs";

@Component({
  selector: 'app-register-organization',
  templateUrl: './register-organization.component.html',
  styleUrls: ['./register-organization.component.scss']
})
export class RegisterOrganizationComponent {
  registerState = RegisterState.ShouldRegister;
  loading = false;

  email = new FormControl('', [Validators.required, Validators.email]);
  organizationName = new FormControl('', [Validators.required]);
  password = new FormControl('', [Validators.required, passwordValidator()]);
  checker = new FormErrorChecker();

  constructor(private identityService: IdentityService, private snackBar: MatSnackBar) {}

  get submitIsDisabled(): boolean {
    return this.email.invalid || this.password.invalid || this.organizationName.invalid;
  }

  register() {
    const registerOrganizationRequestDto = new RegisterOrganizationRequestDto(this.email.value ?? '',
      this.password.value ?? '', this.organizationName.value ?? '');

    this.setLoading();
    this.identityService.registerOrganization(registerOrganizationRequestDto)
      .pipe(take(1)).subscribe({
        next: _ => {
          this.registerState = RegisterState.RegisteredSuccessfully;
          this.loaded();
        },
        error: _ => {
          this.snackBar.openFromComponent(ErrorSnackbarComponent, {duration: 1.5 * 1000});

          this.loaded();
        }
      });
  }

  shouldRegister() {
    return this.registerState == RegisterState.ShouldRegister;
  }

  registeredSuccessfully() {
    return this.registerState == RegisterState.RegisteredSuccessfully;
  }

  invitationNotFound() {
    return this.registerState == RegisterState.InvitationNotFound;
  }

  setLoading() {
    this.loading = true;
  }

  loaded() {
    this.loading = false;
  }
}
