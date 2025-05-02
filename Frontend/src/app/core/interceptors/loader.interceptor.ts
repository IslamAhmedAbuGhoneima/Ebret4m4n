import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs';
let counter = 0;

export const loaderInterceptor: HttpInterceptorFn = (req, next) => {
  const spinner = inject(NgxSpinnerService);
  counter++;
  spinner.show();
  return next(req).pipe(
    //request
    finalize(() => {
      counter--;
      if (counter == 0) spinner.hide();
    })
  );
};
