import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class UserModalService {
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
    const httpOptions = { headers: new HttpHeaders({ "Authorization": this._token }), "responseType": 'text' as 'json' };
    const response = this.client.post<string>(this._baseUrl + 'api/User/EditUser', formData, httpOptions);
    return response;
  }




}

