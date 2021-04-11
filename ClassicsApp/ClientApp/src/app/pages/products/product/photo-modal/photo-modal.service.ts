import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: "root",
})

export class PhotoModalService {
  private _baseUrl: string;

  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  public uploadPhoto(formData: FormData): Observable<any> {
    const response = this.client.post(this._baseUrl + 'api/Product/UploadPhoto', formData);
    return response;
  }

  public removePhoto(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Product/DisableProductPhoto', formData, { responseType: 'text' as 'json' });
    return response;
  }

}

