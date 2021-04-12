import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class AccountModalService {
  private _baseUrl: string;
  public _token: string;


  constructor(private client: HttpClient, public _activatedRouter: ActivatedRoute, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;

    this.checkIfIsLogged();
    this._token = 'Bearer ' + localStorage.getItem('token');
  }

  checkIfIsLogged() {
    if (localStorage.getItem('token') == '' || localStorage.getItem('token') == null) {
      this._router.navigate(['../login'], { relativeTo: this._activatedRouter })
    }
  }

  public editUser(formData: FormData): Observable<string> {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Accept': 'application/json', 'Authorization': this._token
    });

    const response = this.client.post<string>(this._baseUrl + 'api/User/AddUser', formData, { responseType: 'text' as 'json' });
    return response;
  }

  public async getAddress(postalCode: string): Promise<any> {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Accept': 'application/json', 'Authorization': this._token
    });

    const promise = await this.client.get<any>(this._baseUrl + "api/User/GetAddress?postalCode=" + postalCode, { headers }).toPromise();

    return promise;
  }


}

