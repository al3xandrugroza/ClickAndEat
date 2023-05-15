import { Component } from '@angular/core';
import {OAuthService} from "angular-oauth2-oidc";
import {authCodeFlowConfig, AuthConsts} from "../auth/auth-consts";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(private oauthService: OAuthService,
              private router: Router) {}

  isLoggedIn(): boolean {
    const claims = this.oauthService.getIdentityClaims();

    if (!claims) return false;
    return true
  }

  registerNewOrganization() {
    this.router.navigate(['register/organization']);
  }

  waitingForToken(): boolean {
    return sessionStorage.getItem(AuthConsts.AlreadyConfigured) === AuthConsts.AlreadyFetched;
  }

  async loginCode() {
    this.oauthService.configure(authCodeFlowConfig);
    await this.oauthService.loadDiscoveryDocument();
    sessionStorage.setItem(AuthConsts.AlreadyConfigured, AuthConsts.ShouldRetry);

    this.oauthService.initLoginFlow();

    this.oauthService.setupAutomaticSilentRefresh();
  }
}
