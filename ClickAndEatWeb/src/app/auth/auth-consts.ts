import {AuthConfig} from "angular-oauth2-oidc";

export enum AuthConsts {
  AlreadyConfigured = "already_configured",
  ShouldRetry = "should_retry",
  AlreadyFetched = "already_fetched",
  Revoked = "revoked"
}

export const authCodeFlowConfig: AuthConfig = {
  issuer: 'https://localhost:7255',
  // issuer: 'https://localhost:5001',
  clientId: 'web', // The "Auth Code + PKCE" client
  // clientId: '708778530804-rhu8gc4kged3he14tbmonhmhe7a43hlp.apps.googleusercontent.com',
  responseType: 'code',
  // redirectUri: 'https://localhost:4200/',
  redirectUri: window.location.origin + '/',
  silentRefreshRedirectUri: window.location.origin + '/silent-refresh.html',
  scope: 'openid offline_access email profile', // Ask offline_access to support refresh token refreshes
  // scope: 'openid email',
  useSilentRefresh: true, // Needed for Code Flow to suggest using iframe-based refreshes
  silentRefreshTimeout: 5 * 1000, // For faster testing
  timeoutFactor: 0.25, // For faster testing
  sessionChecksEnabled: true,
  showDebugInformation: true, // Also requires enabling "Verbose" level in devtools
  clearHashAfterLogin: false, // https://github.com/manfredsteyer/angular-oauth2-oidc/issues/457#issuecomment-431807040,
  nonceStateSeparator : 'semicolon' // Real semicolon gets mangled by Duende ID Server's URI encoding
}
