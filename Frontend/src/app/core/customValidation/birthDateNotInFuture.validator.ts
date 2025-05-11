import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export const birthDateNotInFutureValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const day = +control.get('day')?.value;
  const month = +control.get('month')?.value; // الشهر دلوقتي رقم من 1 إلى 12
  const year = +control.get('year')?.value;

  if (!day || !month || !year) return null;

  // لازم نطرح 1 من الشهر لأن Date بيستخدم صفر لبداية الأشهر
  const birthDate = new Date(year, month - 1, day);
  const today = new Date();

  birthDate.setHours(0, 0, 0, 0);
  today.setHours(0, 0, 0, 0);

  // التحقق من صحة التاريخ فعلاً
  if (
    birthDate.getDate() !== day ||
    birthDate.getMonth() !== month - 1 ||
    birthDate.getFullYear() !== year
  ) {
    return { invalidDate: true };
  }

  if (birthDate.getTime() > today.getTime()) {
    return { futureDate: true };
  }

  return null;
};
