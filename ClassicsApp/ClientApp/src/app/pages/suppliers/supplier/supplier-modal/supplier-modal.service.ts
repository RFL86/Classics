import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class SupplierModalService {
  private _baseUrl: string;

  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  public editSupplier(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Supplier/EditSupplier', formData, { responseType: 'text' as 'json' });
    return response;
  }

  public createSupplier(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Supplier/AddSupplier', formData, { responseType: 'text' as 'json' });
    return response;
  }

  public getSupplier(supplierId: string): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });

    return this.client.get<any>(this._baseUrl + 'api/Supplier/GetById?modelId=' + supplierId, { headers });
  }




}

