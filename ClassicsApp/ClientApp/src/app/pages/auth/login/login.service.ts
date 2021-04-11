import { Injectable, Inject, EventEmitter } from "@angular/core";
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";



@Injectable({
  providedIn: "root"
})

export class LoginService {   
  private _baseUrl: string;

  constructor(private client: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this._baseUrl = baseUrl; // https://localhost:44365
  }


  public doLogin(formData: FormData): Observable<any> {
    const response = this.client.post(this._baseUrl + 'api/Login/DoLogin', formData);
    return response;
  }


}
