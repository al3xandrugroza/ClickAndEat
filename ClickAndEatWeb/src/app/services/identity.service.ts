import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {InvitationRequestDto} from "../dtos/invitation.request.dto";
import {Observable} from "rxjs";
import {InvitationResponseDto} from "../dtos/invitation.response.dto";
import {RegisterWithInvitationRequestDto} from "../dtos/register-with-invitation.request.dto";
import {RegisterOrganizationRequestDto} from "../dtos/register-organization.request.dto";

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  private identityUrl = '/identity';
  private connectUrl = '/connect';

  constructor(private http: HttpClient) {}

  sendInvitation(invitationRequestDto: InvitationRequestDto): Observable<InvitationResponseDto> {
    return this.http.post<InvitationResponseDto>(`${this.identityUrl}/invitation`, invitationRequestDto);
  }

  registerWithInvitation(registerWithInvitationRequestDto: RegisterWithInvitationRequestDto, invitationCode: string): Observable<any> {
    return this.http.post(`${this.connectUrl}/register/invitation/${invitationCode}`, registerWithInvitationRequestDto);
  }

  getInvitation(invitationCode: string): Observable<InvitationResponseDto> {
    return this.http.get<InvitationResponseDto>(`${this.identityUrl}/invitation?invitationCode=${invitationCode}`);
  }

  registerOrganization(registerOrganizationRequestDto: RegisterOrganizationRequestDto): Observable<any> {
    return this.http.post(`${this.connectUrl}/register`, registerOrganizationRequestDto);
  }
}
