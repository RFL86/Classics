import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class MyCarService {
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

  public getMyCarsTable(): Observable<any> {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Accept': 'application/json', 'Authorization': this._token
    });

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

    const httpOptions = { headers: new HttpHeaders({ "Authorization": this._token }), "responseType": 'text' as 'json' };
    const response = this.client.post<string>(this._baseUrl + 'api/MyCar/AddMyCar', formData, httpOptions);
    return response;
  }

  public editMyCar(formData: FormData): Observable<string> {

    const httpOptions = { headers: new HttpHeaders({ "Authorization": this._token }), "responseType": 'text' as 'json' };
    const response = this.client.post<string>(this._baseUrl + 'api/MyCar/editMyCar', formData, httpOptions);
    return response;
  }

}

