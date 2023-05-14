import {InvitationRequestDto} from "../dtos/invitation.request.dto";

export class InvitationRequestBuilder {
  private invitationRequestDto: InvitationRequestDto;

  public static init(): InvitationRequestBuilder {
    return new InvitationRequestBuilder();
  }

  public anInvitation(): InvitationRequestBuilder {
    this.invitationRequestDto = new InvitationRequestDto();

    return this;
  }

  public withEmail(email: string): InvitationRequestBuilder {
    this.invitationRequestDto.Email = email;

    return this;
  }

  public withRole(role: string): InvitationRequestBuilder {
    this.invitationRequestDto.Role = role;

    return this;
  }

  public build(): InvitationRequestDto {
    return this.invitationRequestDto;
  }
}
