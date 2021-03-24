import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {

  constructor(private injector: Injector, private router: Router) { }
  intercept(req, next) {

    let auths = this.injector.get(AuthService)
    let tokenizedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${auths.getToken()}`
      }
    })
    //return next.handle(tokenizedReq)
    return next.handle(tokenizedReq).pipe(
      catchError((error) => {
        let handled: boolean = false;
        if (error instanceof HttpErrorResponse) {
          if (error.error instanceof ErrorEvent) {
            console.error("Error Event");
          } else {
            console.log(`error status : ${error.status} ${error.statusText}`);
            switch (error.status) {
              case 401:      //login
                this.router.navigateByUrl("");
                console.log(`redirect to login`);
                handled = true;
                break;
              case 403:     //forbidden
                this.router.navigateByUrl("");
                console.log(`redirect to login`);
                handled = true;
                break;
            }
          }
        }
        else {
          console.error("Other Errors");
        }

        if (handled) {
          console.log('return back ');
          return of(error);
        }
        else {
          console.log('throw error back to to the subscriber');
          return throwError(error);
        }

      })
    )
  }
}


