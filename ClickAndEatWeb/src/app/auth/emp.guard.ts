import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import {OAuthService} from "angular-oauth2-oidc";
import jwtDecode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class EmpGuard implements CanActivate {
  constructor(private router: Router, private oauthService: OAuthService) {
  }
  canActivate() {
    if (this.isEmp()) {
      return true;
    }

    this.router.navigate(['']);
    return false;
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
