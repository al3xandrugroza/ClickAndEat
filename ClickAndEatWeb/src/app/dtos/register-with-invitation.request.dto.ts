export class RegisterWithInvitationRequestDto {
  Password: string;

  constructor(password: string) {
    this.Password = password;
  }
}
