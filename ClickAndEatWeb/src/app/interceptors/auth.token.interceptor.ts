import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {OAuthService} from "angular-oauth2-oidc";

@Injectable()
export class AuthTokenInterceptor implements HttpInterceptor {
  constructor(private oauthService: OAuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authReq : HttpRequest<any>;

    if (AuthTokenInterceptor.isValidDestination(req.url) && this.oauthService.hasValidAccessToken() && this.oauthService.hasValidIdToken()) {
      const token = this.oauthService.getAccessToken();
      authReq = req.clone({ setHeaders: { authorization: `Bearer ${token}` } });
    } else {
      authReq = req;
    }

    return next.handle(authReq);
  }

  private static isValidDestination(url: string): boolean {
    const identityUrl = '/identity';
    const apiUrl = '/api';

    return url.startsWith(identityUrl) || url.startsWith(apiUrl);
  }
}
