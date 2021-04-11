import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class AlertModalService {
  private _baseUrl: string;

  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  public createAlert(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Alert/AddAlert', formData, { responseType: 'text' as 'json' });
    return response;
  }

}

