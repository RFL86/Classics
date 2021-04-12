import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router, ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class ProductService {
  private _baseUrl: string;
  public _token: string;


  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string, public _activatedRouter: ActivatedRoute) {
    this._baseUrl = baseUrl;
    this.checkIfIsLogged();
    this._token = 'Bearer ' + localStorage.getItem('token');
  }

  checkIfIsLogged() {
    if (localStorage.getItem('token') == '' || localStorage.getItem('token') == null) {
      this._router.navigate(['../login'], { relativeTo: this._activatedRouter })
    }
  }

  public getProducts(): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Accept': 'application/json', 'Authorization': this._token
    });
    return this.client.get<any>(this._baseUrl + 'api/Product/GetUserProducts', { headers });
  }

}

