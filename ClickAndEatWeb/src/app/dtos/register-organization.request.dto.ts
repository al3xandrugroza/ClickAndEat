export class RegisterOrganizationRequestDto {
  Email: string;
  Password: string;
  OrganizationName: string;

  constructor(email: string, password: string, organizationName: string) {
    this.Email = email;
    this.Password = password;
    this.OrganizationName = organizationName;
  }
}
