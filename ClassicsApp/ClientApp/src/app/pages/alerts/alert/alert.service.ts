import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';


@Injectable({
  providedIn: "root",
})

export class AlertService {
  private _baseUrl: string;

  constructor(private client: HttpClient, @Inject('BASE_URL')
  baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  public getAlerts(): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json', });

    return this.client.get<any>(this._baseUrl + 'api/Alert/GetAlerts', { headers });
  }

  public changeAlertStatus(formData: FormData): Observable<string> {
    const response = this.client.post<string>(this._baseUrl + 'api/Alert/ChangeAlertStatus', formData, { responseType: 'text' as 'json' });
    return response;
  }

}

