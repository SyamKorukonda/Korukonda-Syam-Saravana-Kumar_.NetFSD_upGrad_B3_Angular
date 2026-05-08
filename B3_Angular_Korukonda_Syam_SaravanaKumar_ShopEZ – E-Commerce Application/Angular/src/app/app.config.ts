import { ApplicationConfig } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { authInterceptor } from './interceptors/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    // Router with all ShopEZ routes
    
    provideRouter(routes, withComponentInputBinding()),
    // HttpClient with JWT interceptor — auto attaches Bearer token

    provideHttpClient(withInterceptors([authInterceptor])),
    provideAnimations()
  ]
};