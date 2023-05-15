import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {OAuthService} from "angular-oauth2-oidc";
import {authCodeFlowConfig, AuthConsts} from "./auth/auth-consts";
import {filter} from "rxjs";
import jwtDecode from "jwt-decode";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private router: Router, private oauthService: OAuthService) {
    if (sessionStorage.getItem(AuthConsts.AlreadyConfigured) === AuthConsts.ShouldRetry) {
      this.loginCode();
    }

    if (sessionStorage.getItem(AuthConsts.AlreadyConfigured) === AuthConsts.AlreadyFetched) {
      this.configureCodeFlow();
    }

    this.oauthService.events
      .pipe(filter((e) => e.type === 'token_received'))
      .subscribe(_ => {
        console.debug('state', this.oauthService.state);
        this.oauthService.loadUserProfile();

        sessionStorage.setItem(AuthConsts.AlreadyConfigured, AuthConsts.TokenReceived);
        const scopes = this.oauthService.getGrantedScopes();
        console.debug('scopes', scopes);

        window.location.href = window.location.origin;
      });
  }

  async loginCode() {
    this.oauthService.configure(authCodeFlowConfig);
    await this.oauthService.loadDiscoveryDocument();
    sessionStorage.setItem(AuthConsts.AlreadyConfigured, AuthConsts.AlreadyFetched);

    this.oauthService.initLoginFlow();

    this.oauthService.setupAutomaticSilentRefresh();
  }

  private configureCodeFlow() {
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.loadDiscoveryDocumentAndTryLogin();

    this.oauthService.setupAutomaticSilentRefresh();
  }

  isLoggedIn(): boolean {
    const claims = this.oauthService.getIdentityClaims();

    if (!claims) return false;
    return true
  }

  logout() {
    this.oauthService.revokeTokenAndLogout();
    this.oauthService.logOut();

    sessionStorage.setItem(AuthConsts.AlreadyConfigured, AuthConsts.Revoked);
  }

  email() {
    const token = this.oauthService.getAccessToken();
    const decodedToken: { 'email': string} = jwtDecode(token);

    return decodedToken['email'];
  }

  isAdmin() {
    const token = this.oauthService.getAccessToken();
    const decodedToken: { 'role': string} = jwtDecode(token);

    return decodedToken['role'] == 'admin';
  }

  goToInvitationForm() {
    this.router.navigate(['dashboard/invitation'])
  }

  goHome() {
    window.location.href = window.location.origin;
  }

  get logo(): string {
    if (!this.oauthService.hasValidAccessToken() || !this.oauthService.hasValidIdToken()) {
      return 'Click&Eat';
    }

    const token = this.oauthService.getAccessToken();
    const decodedToken: { 'org_name': string} = jwtDecode(token);

    return decodedToken['org_name'];
  }
}
