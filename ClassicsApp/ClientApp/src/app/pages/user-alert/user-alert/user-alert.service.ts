import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class UserAlertService {
  private _baseUrl: string;

  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;

  }

  public getAlerts(): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });

    return this.client.get<any>(this._baseUrl + 'api/Alert/GetUserAlerts', { headers });
  }

  public changeAlertStatus(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Alert/ChangeUserAlertStatus', formData, { responseType: 'text' as 'json' });
    return response;
  }

}

