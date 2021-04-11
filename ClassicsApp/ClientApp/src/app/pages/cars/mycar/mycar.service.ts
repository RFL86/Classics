import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class MyCarService {
  private _baseUrl: string;

  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;

  }

  public getMyCarsTable(): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });

    return this.client.get<any>(this._baseUrl + 'api/MyCar/GetMyCars', { headers });
  }

  public getBrands(): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });

    return this.client.get<any>(this._baseUrl + 'api/Car/GetActiveBrands', { headers });
  }

  public getModelsByBrandId(brandId: string): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });

    return this.client.get<any>(this._baseUrl + 'api/Car/GetActiveModels?brandId=' + brandId, { headers });
  }

  public getSeriesByModelId(modelId: string): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });

    return this.client.get<any>(this._baseUrl + 'api/Car/GetActiveSeries?modelId=' + modelId, { headers });
  }

  public addMyCar(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/MyCar/AddMyCar', formData, { responseType: 'text' as 'json' });
    return response;
  }

  public editMyCar(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/MyCar/editMyCar', formData, { responseType: 'text' as 'json' });
    return response;
  }

}

