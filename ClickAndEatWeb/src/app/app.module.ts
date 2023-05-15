import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import {OAuthModule} from "angular-oauth2-oidc";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {ErrorSnackbarComponent} from "./snackbars/error-snackbar/error-snackbar.component";
import {SuccessSnackbarComponent} from "./snackbars/success-snackbar/success-snackbar.component";
import {RegisterWithInvitationComponent} from "./register-with-invitation/register-with-invitation.component";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatMenuModule} from "@angular/material/menu";
import {MatGridListModule} from "@angular/material/grid-list";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatFormFieldModule} from "@angular/material/form-field";
import {ReactiveFormsModule} from "@angular/forms";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {MatSelectModule} from "@angular/material/select";
import {MatInputModule} from "@angular/material/input";
import {AuthTokenInterceptor} from "./interceptors/auth.token.interceptor";
import {RegisterOrganizationComponent} from "./register-organization/register-organization.component";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegisterWithInvitationComponent,
    ErrorSnackbarComponent,
    RegisterOrganizationComponent,
    SuccessSnackbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    OAuthModule.forRoot(),
    HttpClientModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatGridListModule,
    MatProgressBarModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSnackBarModule,
    MatSelectModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthTokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
