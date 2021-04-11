import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class CarModalService {
  private _baseUrl: string;

  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  public editItem(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Car/EditItem', formData, { responseType: 'text' as 'json' });
    return response;
  }



}

