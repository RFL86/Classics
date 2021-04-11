import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class MyCarModalService {
  private _baseUrl: string;

  constructor(private client: HttpClient, private _router: Router, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  public editMyCar(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/MyCar/EditMyCar', formData, { responseType: 'text' as 'json' });
    return response;
  }

  public removeMyCar(formData: FormData): void {

    this.client.post<void>(this._baseUrl + 'api/MyCar/RemoveMyCar', formData, { responseType: 'text' as 'json' }).subscribe();
  }



}

