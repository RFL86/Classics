import { Injectable, Inject } from "@angular/core";
import { Router } from '@angular/router';

@Injectable({
  providedIn: "root",
})

export class RedirectService{
  constructor(private _router: Router) {
  }

  public checkIfIsAuth() {
    if (localStorage.getItem('token') == '' || localStorage.getItem('token') == null) {
      this._router.navigateByUrl('../login');
    }   
  }

  public checkIfProfileIsLoad() {
    if (localStorage.getItem('postalCode') == '' || localStorage.getItem('postalCode') == null) {
      this._router.navigateByUrl('/account');
    }
  }

}

