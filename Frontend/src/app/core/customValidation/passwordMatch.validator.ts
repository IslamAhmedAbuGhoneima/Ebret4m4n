import {
  AbstractControl,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';

export const passwordMatch: ValidatorFn = (
  formGroup: AbstractControl
): ValidationErrors | null => {
  let passInput = formGroup.get('password');
  let confirmPassInput = formGroup.get('confirmPassword');
  if (
    !passInput ||
    !confirmPassInput ||
    !passInput.value ||
    !confirmPassInput.value
  )
    return null;
  let valueError = {
    unMatchedPassword: {
      pass: passInput.value,
      confirmPass: confirmPassInput.value,
    },
  };
  return passInput.value === confirmPassInput.value ? null : valueError;
};
