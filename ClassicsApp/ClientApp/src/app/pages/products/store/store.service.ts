import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class StoreService {
  private _baseUrl: string;
  public _token: string;

  constructor(private client: HttpClient, public _activatedRouter: ActivatedRoute, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;

    this._token = 'Bearer ' + localStorage.getItem('token');
  }

  public getUserModels(): Observable<any> {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Accept': 'application/json', 'Authorization': this._token
    });

    return this.client.get<any>(this._baseUrl + 'api/Car/GetUserCars', { headers });
  }

  public getProducts(modelId): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });
    return this.client.get<any>(this._baseUrl + 'api/Product/GetByModel?modelId=' + modelId, { headers });
  }

}

