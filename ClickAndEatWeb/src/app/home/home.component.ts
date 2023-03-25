import { Component } from '@angular/core';
import {OAuthService} from "angular-oauth2-oidc";
import {authCodeFlowConfig, AuthConsts} from "../auth/auth-consts";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(private oauthService: OAuthService) {}

  get hasValidAccessToken() {
    return this.oauthService.hasValidAccessToken();
  }

  get hasValidIdToken() {
    return this.oauthService.hasValidIdToken();
  }

  get idClaims() {
    return this.oauthService.getIdentityClaims();
  }

  get givenName() {
    var claims = this.oauthService.getIdentityClaims();
    if (!claims) return null;
    return claims['given_name'];
  }

  get id_token() {
    return this.oauthService.getIdToken();
  }

  get access_token() {
    return this.oauthService.getAccessToken();
  }

  get id_token_expiration() {
    return this.oauthService.getIdTokenExpiration();
  }

  get access_token_expiration() {
    return this.oauthService.getAccessTokenExpiration();
  }

  async loginCode() {
    this.oauthService.configure(authCodeFlowConfig);
    await this.oauthService.loadDiscoveryDocument();
    sessionStorage.setItem(AuthConsts.AlreadyConfigured, AuthConsts.Code);

    this.oauthService.initLoginFlow();

    this.oauthService.setupAutomaticSilentRefresh();
  }

  logout() {
    this.oauthService.revokeTokenAndLogout();
    this.oauthService.logOut();
  }
}
