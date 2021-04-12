import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router, ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';
import { SelectItem } from "./product-modal.model"



@Injectable({
  providedIn: "root",
})

export class ProductModalService {
  private _baseUrl: string;
  public _token: string;


  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string, public _activatedRouter: ActivatedRoute) {
    this._baseUrl = baseUrl;

    this.checkIfIsLogged();
    this._token = 'Bearer ' + localStorage.getItem('token');
  }

  checkIfIsLogged() {
    if (localStorage.getItem('token') == '' || localStorage.getItem('token') == null) {
      this._router.navigate(['../login'], { relativeTo: this._activatedRouter })
    }
  }

  public editProduct(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Product/EditProduct', formData, { responseType: 'text' as 'json' });
    return response;
  }

  public createProduct(formData: FormData): Observable<string> {

    const httpOptions = { headers: new HttpHeaders({ "Authorization": this._token }), "responseType": 'text' as 'json' };

    const response = this.client.post<string>(this._baseUrl + 'api/Product/AddProduct', formData, httpOptions);
    return response;
  }

  public getProduct(supplierId: string): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });

    return this.client.get<any>(this._baseUrl + 'api/Product/GetById?modelId=' + supplierId, { headers });
  }

  public getCarModels(): Observable<SelectItem[]> {
    const headers = new HttpHeaders().set("content-type", "application/json");
    const inconsistencies = this.client.get<SelectItem[]>(this._baseUrl + "api/Car/GetModelList", { headers });

    return inconsistencies;
  }

}

