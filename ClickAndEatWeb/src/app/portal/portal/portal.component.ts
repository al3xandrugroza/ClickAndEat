import { Component } from '@angular/core';
import {OAuthService} from "angular-oauth2-oidc";
import jwtDecode from "jwt-decode";

@Component({
  selector: 'app-portal',
  templateUrl: './portal.component.html',
  styleUrls: ['./portal.component.scss']
})
export class PortalComponent {
  constructor(private oauthService: OAuthService) {
  }

  isAdmin() {
    if (!this.oauthService.hasValidAccessToken() || !this.oauthService.hasValidIdToken()) {
      return false;
    }

    const token = this.oauthService.getAccessToken();
    const decodedToken: { 'role': string} = jwtDecode(token);

    return decodedToken['role'] == 'admin';
  }

  isEmp() {
    if (!this.oauthService.hasValidAccessToken() || !this.oauthService.hasValidIdToken()) {
      return false;
    }

    const token = this.oauthService.getAccessToken();
    const decodedToken: { 'role': string} = jwtDecode(token);

    return decodedToken['role'] == 'emp';
  }
}
