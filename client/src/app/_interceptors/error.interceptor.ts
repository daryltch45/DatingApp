import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const router = inject(Router); 
  const toastr = inject(ToastrService); 

  

  return next(req).pipe(
    catchError(
      error=>{
        if(error){
          switch(error){
            case 400: 
            if(error.error.errors){
              const modalStateErrors = [];
              for(const key in error.error.errors){
                modalStateErrors.push(error.error.errors[key])
              }
            }
            throw modalStateErrors.flat(); 
            {
              toastr.error(error.error, error.status)
            }
            break; 
            case 401:
              toastr.error('Unauthorised', error.status)
              break; 
            case 404: 
              router.navigateByUrl('/not-found'); 
              break; 
            case 500: 
              router.navigateByUrl('/not-found'); 
            default: 
            break; 
          }
        }
      }
    )
  );
};
