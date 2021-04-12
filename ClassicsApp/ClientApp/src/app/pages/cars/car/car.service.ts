import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class CarService {
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

  public getBrandsTable(): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });

    return this.client.get<any>(this._baseUrl + 'api/Car/GetBrandsTable', { headers });
  }

  public getModelsTable(): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });
    const response = this.client.get<any>(this._baseUrl + 'api/Car/GetModelsTable', { headers });

    return response;
  }

  public getSeriesTable(): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json',});

    return this.client.get<any>(this._baseUrl + 'api/Car/GetSeriesTable', { headers });
  }

  public getBrands(): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', }); 

    return this.client.get<any>(this._baseUrl + 'api/Car/GetBrands', { headers });
  }

  public getModelsByBrandId(brandId: string): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });

    return this.client.get<any>(this._baseUrl + 'api/Car/GetModels?brandId=' + brandId, { headers });
  }

  public addSerie(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Car/AddSerie', formData, { responseType: 'text' as 'json' });
    return response;
  }

  public addModel(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Car/AddCarModel', formData, { responseType: 'text' as 'json' });
    return response;
  }

  public addBrand(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Car/AddBrand', formData, { responseType: 'text' as 'json' });
    return response;
  }
}

