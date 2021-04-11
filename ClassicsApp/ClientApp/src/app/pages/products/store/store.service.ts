import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class StoreService {
  private _baseUrl: string;

  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  public getProducts(): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });
    return this.client.get<any>(this._baseUrl + 'api/Product/GetAllProducts', { headers });
  }

}

