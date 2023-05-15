import { Component } from '@angular/core';
import {FormErrorChecker} from "../../utils/form.error.checker";
import {FormControl, Validators} from "@angular/forms";
import {IdentityService} from "../../services/identity.service";
import {InvitationRequestBuilder} from "../../builders/invitation.request.builder";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ErrorSnackbarComponent} from "../../snackbars/error-snackbar/error-snackbar.component";
import {HttpStatusCode} from "@angular/common/http";
import {take} from "rxjs";

@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrls: ['./invitation.component.scss']
})
export class InvitationComponent {
  email = new FormControl('', [Validators.required, Validators.email]);
  role = new FormControl('', [Validators.required]);
  checker = new FormErrorChecker();

  invitationSent: boolean = false;
  invitedEmail: string;
  invitedWithRole: string;
  invitationCode: string;
  organizationId: string;

  constructor(private identityService: IdentityService, private snackBar: MatSnackBar) {}

  sendInvitation() {
    const invitation = InvitationRequestBuilder.init().anInvitation()
      .withEmail(this.email.value ?? '')
      .withRole(this.role.value ?? '')
      .build();

    this.identityService.sendInvitation(invitation).pipe(take(1)).subscribe({
      next: response => {
        this.invitedEmail = response.Email;
        this.invitedWithRole = response.Role;
        this.invitationCode = response.Identifier;
        this.organizationId = response.OrganizationId;

        this.invitationSent = true;
      },
      error: err => {
        this.snackBar.openFromComponent(ErrorSnackbarComponent, {
          duration: 1.5 * 1000
        })
      }
    });
  }

  get submitIsDisabled(): boolean {
    return this.email.invalid || this.role.invalid;
  }

  sendAnotherInvitation() {
    this.email.reset();
    this.role.reset();

    this.invitationSent = false;
  }

  getInvitationLink() {
    return `https://localhost:4200/register/invitation?invitationCode=${this.invitationCode}`;
  }
}
