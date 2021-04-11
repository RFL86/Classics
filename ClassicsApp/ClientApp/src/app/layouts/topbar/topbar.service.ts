import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from '@angular/router';


@Injectable({
  providedIn: "root",
})

export class TopBarService{
  private _baseUrl: string;

  constructor(private _http: HttpClient, private _router: Router, @Inject('BASE_URL') 
  baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  logout() {
    localStorage.setItem('token', '');
    localStorage.setItem('name', '');
    localStorage.setItem('profile', '');
    localStorage.setItem('postalCode', ''); 

    this._router.navigateByUrl('/login');

    //return this._http.get(this._baseUrl + 'Api/Login/logout').subscribe(result => {
    //  this._router.navigateByUrl('/login');
    //});
  }

}

