import { Injectable } from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {OAuthService} from "angular-oauth2-oidc";

@Injectable({
  providedIn: 'root'
})
export class NonAuthGuard implements CanActivate {

  constructor(private router: Router, private oauthService: OAuthService) {}

  canActivate() {
    if (this.oauthService.hasValidAccessToken() && this.oauthService.hasValidIdToken()) {
      this.router.navigate(['/dashboard']);
      return false;
    }

    return true;
  }
}
