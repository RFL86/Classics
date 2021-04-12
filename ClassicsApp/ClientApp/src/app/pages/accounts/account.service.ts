import { Injectable, Inject } from "@angular/core";
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: "root"
})

export class AccountService {   
  private _baseUrl: string;
  public _token: string;

  constructor(private client: HttpClient, public _activatedRouter: ActivatedRoute, private _router: Router, @Inject("BASE_URL") baseUrl: string) {
    this._baseUrl = baseUrl; // https://localhost:44365
    this.checkIfIsLogged();
    this._token = 'Bearer ' + localStorage.getItem('token');
  }

  checkIfIsLogged() {
    if (localStorage.getItem('token') == '' || localStorage.getItem('token') == null) {
      this._router.navigate(['../login'], { relativeTo: this._activatedRouter })
    }
  }

  public getUser(): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Accept': 'application/json', 'Authorization': this._token
    });
    return this.client.get<any>(this._baseUrl + 'api/User/GetById', { headers });
  }

}
