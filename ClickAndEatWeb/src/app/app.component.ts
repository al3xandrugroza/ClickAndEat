import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {OAuthService} from "angular-oauth2-oidc";
import {authCodeFlowConfig, AuthConsts} from "./auth/auth-consts";
import {filter} from "rxjs";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private router: Router, private oauthService: OAuthService) {
    if (sessionStorage.getItem(AuthConsts.AlreadyConfigured) === AuthConsts.Code) {
      this.configureCodeFlow();
    }

    this.oauthService.events
      .pipe(filter((e) => e.type === 'token_received'))
      .subscribe(_ => {
        console.debug('state', this.oauthService.state);
        this.oauthService.loadUserProfile();

        const scopes = this.oauthService.getGrantedScopes();
        console.debug('scopes', scopes);
      });
  }

  private configureCodeFlow() {
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.loadDiscoveryDocumentAndTryLogin();

    this.oauthService.setupAutomaticSilentRefresh();
  }
}
