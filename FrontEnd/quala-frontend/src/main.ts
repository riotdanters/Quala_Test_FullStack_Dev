import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { importProvidersFrom } from '@angular/core';
import { NZ_I18N, es_ES } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import es from '@angular/common/locales/es';

import { AppComponent } from './app/app.component';
import { routes } from './app/app.routes';
import { jwtInterceptor } from './app/core/interceptors/jwt.interceptor'; // Asumiendo que lo conviertes a un interceptor funcional
import { NgZorroAntdModule } from './app/shared/ng-zorro-antd.module';
import { NzMessageService } from 'ng-zorro-antd/message';

registerLocaleData(es);

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    provideHttpClient(withInterceptors([jwtInterceptor])),
    importProvidersFrom(NgZorroAntdModule), // Sigue importando tu módulo de UI compartido
    { provide: NZ_I18N, useValue: es_ES },
    NzMessageService
  ]
}).catch(err => console.error(err));