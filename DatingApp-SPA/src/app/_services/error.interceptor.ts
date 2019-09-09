import {HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS, HttpRequest, HttpHandler, HttpEvent} from '@angular/common/http';
import {Injectable} from '@angular/core';
import { catchError } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        // check if the error is a 401 error
        if (error.status === 401 ) {
          return throwError(error.statusText);
        }
        if (error instanceof HttpErrorResponse) {
          const appError = error.headers.get('Application-Error');
          if (appError) {
            return throwError(appError);
          }
          const serverError = error.error;
          let modelStateErrors = '';
          if (serverError.errors && typeof serverError.errors === 'object') {
            for (const key in serverError.errors) {
              if (serverError.errors[key]) {
                modelStateErrors += serverError.errors[key] + '\n';
              }
            }
          }
          return throwError(modelStateErrors || serverError || 'Server Error');
        }
      }));
  }
}


export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};
