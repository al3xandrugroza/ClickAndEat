import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {FormControl, Validators} from "@angular/forms";
import {FormErrorChecker} from "../utils/form.error.checker";
import {MatSnackBar} from "@angular/material/snack-bar";
import {IdentityService} from "../services/identity.service";
import {RegisterWithInvitationRequestDto} from "../dtos/register-with-invitation.request.dto";
import {ErrorSnackbarComponent} from "../snackbars/error-snackbar/error-snackbar.component";
import {passwordValidator} from "../utils/password-validator.directive";
import {RegisterState} from "../utils/consts";
import {HttpStatusCode} from "@angular/common/http";
import {take} from "rxjs";

@Component({
  selector: 'app-register-with-invitation',
  templateUrl: './register-with-invitation.component.html',
  styleUrls: ['./register-with-invitation.component.scss']
})
export class RegisterWithInvitationComponent implements OnInit {
  registerState = RegisterState.ShouldRegister;
  invitationCode: string;
  loading = false;

  email = new FormControl({value: '', disabled: true }, [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required, passwordValidator()]);
  checker = new FormErrorChecker();

  constructor(private route: ActivatedRoute,
              private identityService: IdentityService,
              private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.invitationCode = params['invitationCode'];
    });

    this.setLoading();
    this.identityService.getInvitation(this.invitationCode)
      .pipe(take(1)).subscribe({
        next: invitationDto => {
          this.email.setValue(invitationDto.Email);

          this.loaded();
        },
        error: err => {
          if (err.status == HttpStatusCode.NotFound) {
            this.registerState = RegisterState.InvitationNotFound;
          } else {
            this.snackBar.openFromComponent(ErrorSnackbarComponent, {
              duration: 1.5 * 1000
            })
          }

          this.loaded();
        }
      })
  }

  get submitIsDisabled(): boolean {
    return this.email.invalid || this.password.invalid;
  }

  register() {
    const registerWithInvitationDto = new RegisterWithInvitationRequestDto(this.password.value ?? '');

    this.setLoading();
    this.identityService.registerWithInvitation(registerWithInvitationDto, this.invitationCode)
      .pipe(take(1)).subscribe({
        next: _ => {
          this.registerState = RegisterState.RegisteredSuccessfully;
          this.loaded();
        },
        error: _ => {
          this.snackBar.openFromComponent(ErrorSnackbarComponent, {
            duration: 1.5 * 1000
          });

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
