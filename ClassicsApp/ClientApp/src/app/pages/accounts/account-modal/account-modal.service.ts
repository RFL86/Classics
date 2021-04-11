import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class AccountModalService {
  private _baseUrl: string;

  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  public editUser(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/User/AddUser', formData, { responseType: 'text' as 'json' });
    return response;
  }

  public async getAddress(postalCode: string): Promise<any> {
    const headers = new HttpHeaders().set("content-type", "application/json");
    const promise = await this.client.get<any>(this._baseUrl + "api/User/GetAddress?postalCode=" + postalCode, { headers }).toPromise();

    return promise;
  }


}

