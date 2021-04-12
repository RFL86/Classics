import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class UserAlertService {
  private _baseUrl: string;
  public _token: string;


  constructor(private client: HttpClient, private _router: Router, public _activatedRouter: ActivatedRoute, @Inject('BASE_URL')
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

  public getAlerts(): Observable<any> {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Accept': 'application/json', 'Authorization': this._token
    });

    return this.client.get<any>(this._baseUrl + 'api/Alert/GetUserAlerts', { headers });
  }

  public changeAlertStatus(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Alert/ChangeUserAlertStatus', formData, { responseType: 'text' as 'json' });
    return response;
  }

}

