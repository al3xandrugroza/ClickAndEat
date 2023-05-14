import {ValidationErrors, ValidatorFn} from "@angular/forms";

export function passwordValidator(): ValidatorFn {
  return (passwordControl): ValidationErrors | null => {
    const password = passwordControl.value;
    const containsUpperCase = /[A-Z]/.test(password);
    const containsLowerCase = /[a-z]/.test(password);
    const containsDigit = /[0-9]/.test(password);
    const containsNonAlphaNumeric = /[^A-Za-z0-9]/.test(password);
    const hasMinLen = password.length > 6;

    if (containsUpperCase && containsLowerCase && containsDigit && containsNonAlphaNumeric && hasMinLen) return null;

    return {password: {}};
  }
}
