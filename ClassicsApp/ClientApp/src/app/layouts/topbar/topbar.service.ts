import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from '@angular/router';
import firebase from 'firebase/app';





@Injectable({
  providedIn: "root",
})

export class TopBarService{
  private _baseUrl: string;

  constructor(private _http: HttpClient, private _router: Router, @Inject('BASE_URL') 
  baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  //logout() {
  //  localStorage.setItem('token', '');
  //  localStorage.setItem('name', '');
  //  localStorage.setItem('profile', '');
  //  localStorage.setItem('postalCode', '');
  //  localStorage.setItem('googleToken', '');
  //  this._router.navigateByUrl('/login'); 
  //}


}

