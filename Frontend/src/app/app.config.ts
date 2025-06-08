import {
  ApplicationConfig,
  provideZoneChangeDetection,
  isDevMode,
  importProvidersFrom,
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {
  provideHttpClient,
  withFetch,
  withInterceptors,
} from '@angular/common/http';
import { provideCharts, withDefaultRegisterables } from 'ng2-charts';
import { loaderInterceptor } from './core/interceptors/loader.interceptor';
import { refreshTokenInterceptor } from './core/interceptors/refresh-token.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withFetch(), withInterceptors([])),
    provideAnimationsAsync(),
    provideCharts(withDefaultRegisterables()),
    provideHttpClient(
      withFetch(),
      withInterceptors([loaderInterceptor, refreshTokenInterceptor])
    ),
    importProvidersFrom(
      BrowserAnimationsModule,
      NgxSpinnerModule,
      ToastrModule.forRoot({
        timeOut: 5000,
        positionClass: 'toast-top-center',
        toastClass: 'ngx-toastr',
        preventDuplicates: true,
        countDuplicates: true,
        closeButton: true,
        tapToDismiss: true,
        progressBar: false,
        iconClasses: {
          success: ' ',
        },
      }),
      SweetAlert2Module.forRoot()
    ),
  ],
};
