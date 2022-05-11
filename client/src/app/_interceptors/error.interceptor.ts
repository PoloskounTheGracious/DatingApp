import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';
import { ExtractorHost } from '@angular/compiler';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400: 
              if (error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }   //Flattens the error array we get back from validation responses.
              throw modalStateErrors.flat(); //.flat() is available in es2019 - see tsconfig.json and add "es2019" to use.
              } else {
                this.toastr.error(error.statusText, error.Status);
              }
              break;
            case 401: 
              this.toastr.error(error.statusText, error.status);
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500: 
              const navigationExtras: NavigationExtras = {state: {error: error.error}};
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default: 
              this.toastr.error('Oops! Something went wrong!');
              console.log(error);
              break;
          }
        }
        return throwError(error);
        }
      )
    );
  }
}
